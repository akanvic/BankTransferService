using BankTransferService.Core.Responses;
using BankTransferService.Service.Interface;
using BankTransferService.Service.Utilities;
using Flutterwave.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Implementation
{
    public class FlutterwaveGateway : IFlutterwaveGateway
    {
        public Task<RecipientCreationResponse> CreateTransferReciepient(MainTransferRequest reciepientRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> GetBankList()
        {
            var secretKey = "FLWSECK_TEST-81070ed7e3080a289ab067180d5bd542-X";
            var baseUrl = @"https://api.flutterwave.com/v3/banks/NG";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + secretKey);

                var response = await client.GetAsync(baseUrl);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert.DeserializeObject<ListOfBankResponse>(responseContent);

                if (response.IsSuccessStatusCode)
                    return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };
                return new ResponseModel { StatusCode = response.StatusCode, Msg = "Error" };
            }
        }

        public async Task<ResponseModel> GetTransactionStatus(string reference)
        {
            var secretKey = "FLWSECK_TEST-81070ed7e3080a289ab067180d5bd542-X";

            var baseUrl = @$"https://api.paystack.co/transfer/verify/{reference}";
            GetTransactionStatusReponse serviceResponse = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helper.FlutterwaveSecretKey);

                var response = await client.GetAsync(baseUrl);

                var responseContent = response.Content.ReadAsStringAsync().Result;
                serviceResponse = JsonConvert.DeserializeObject<GetTransactionStatusReponse>(responseContent);
                
                if (response.IsSuccessStatusCode)

                {

                    serviceResponse.Data.Amount = serviceResponse.Data.Amount / 100;

                    if (serviceResponse.Data.TransactionStatus.Equals("success"))
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer was successful", Data = serviceResponse };
                    else if (serviceResponse.Data.TransactionStatus.Equals("pending"))
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer is pending", Data = serviceResponse };
                    else if (serviceResponse.Data.TransactionStatus.Equals("failed"))
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer failed", Data = serviceResponse };
                    else if (serviceResponse.Data.TransactionStatus.Equals("reversed"))
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer reversed", Data = serviceResponse };

                }
                return new ResponseModel { StatusCode = response.StatusCode, Msg = "Something went wrong, an error occured", Data = serviceResponse };
            }
        }

        public async Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest)
        {

            var baseUrl = @"https://api.flutterwave.com/v3/transfers";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Helper.FlutterwaveSecretKey);

                transferRequest.TransactionReference = Helper.GenerateTransactionReference();

                var json = JsonConvert.SerializeObject(transferRequest);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(baseUrl), stringContent);

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var serviceResponse = JsonConvert.DeserializeObject<InitiateTransferResponse>(responseContent);

                if (response.IsSuccessStatusCode)

                {

                    if (serviceResponse.Data.Status.Equals("NEW"))
                    {
                        //await SaveTransaction(transferRequest, serviceResponse, recipientResponse);
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse }; ;
                    }
                    else
                    {
                        // If the transfer failed, retry with exponential backoff

                        int retries = 0;
                        int backoffInterval = 1000; // Initial backoff interval in milliseconds

                        while (retries < transferRequest.MaxRetryAttempt)
                        {
                            // Increase the backoff interval for each retry
                            backoffInterval *= 2;

                            // Wait for the backoff interval before retrying the request
                            Thread.Sleep(backoffInterval);

                            // Make the request again
                            var retryResponse = await client.PostAsync(new Uri(baseUrl), stringContent);
                            var retryResponseContent = retryResponse.Content.ReadAsStringAsync().Result;
                            var retryServiceResponse = JsonConvert.DeserializeObject<InitiateTransferResponse>(retryResponseContent);

                            if (retryServiceResponse.Data.Status.Equals("New"))
                            {
                                // If the request was successful, return true
                                //await SaveTransaction(transferRequest, serviceResponse, recipientResponse);
                                return new ResponseModel { StatusCode = response.StatusCode, Msg = retryServiceResponse.Message, Data = retryServiceResponse }; ;
                            }

                            retries++;
                        }

                        // If all retries have failed, return false
                        return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer was not successful", Data = serviceResponse }; ;
                    }

                }
                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message };
            }
        }

        public async Task<ResponseModel> ValidateAccount(ValidateAccountRequest validateAccount)
        {
            FlutterwaveApi api = new FlutterwaveApi(Helper.FlutterwaveSecretKey);

            VerifyBankAccountResponse responses = api.Miscellaneous.VerifyBankAccount
                (validateAccount.AccountNumber, validateAccount.Code);

            var secretKey = "FLWSECK_TEST-81070ed7e3080a289ab067180d5bd542-X";
            var baseUrl = @"https://api.flutterwave.com/v3/accounts/resolve";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + secretKey);

                var json = JsonConvert.SerializeObject(validateAccount);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri(baseUrl), stringContent);
                var responseContent1 = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)

                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    var serviceResponse = JsonConvert.DeserializeObject<ValidateAccountResponse>(responseContent);

                    return new ResponseModel { StatusCode = response.StatusCode, Msg = "Successful", Data = serviceResponse };
                }
                return new ResponseModel { StatusCode = response.StatusCode, Msg = "Error" };
            }
        }


    }
}
