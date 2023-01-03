using BankTransferService.Core.Entities;
using BankTransferService.Core.Responses;
using BankTransferService.Core.Responses.Flutterwave.Request;
using BankTransferService.Core.Responses.Paystack;
using BankTransferService.Core.Responses.Paystack.Request;
using BankTransferService.Repo.Data.Repository.Interfaces;
using BankTransferService.Service.Interface;
using BankTransferService.Service.Utilities;
using Flutterwave.Net;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Implementation
{
    public class FlutterwaveGateway : IFlutterwaveGateway
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
                            Policy<HttpResponseMessage>
                                .Handle<HttpRequestException>()
                                .OrResult(c => c.StatusCode >= HttpStatusCode.InternalServerError || HttpStatusCode.RequestTimeout == c.StatusCode)
                                .WaitAndRetryAsync(Backoff.ExponentialBackoff(TimeSpan.FromSeconds(1), 5));
        public FlutterwaveGateway(IHttpClientFactory httpClientFactory, ITransactionRepo transactionRepo)
        {
            _httpClientFactory = httpClientFactory;
            _transactionRepo = transactionRepo;
        }

        public async Task<ResponseModel> GetBankList()
        {
            var url = @"banks/NG";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.FlutterwaveSecretKey, Helper.FlutterwavBaseURL, _httpClientFactory);

            var response = await client.GetAsync(url);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var serviceResponse = JsonConvert.DeserializeObject<ListOfBankResponse>(responseContent);
            
            if (response.IsSuccessStatusCode)
                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };
            return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message };
        }

        public async Task<ResponseModel> GetTransactionStatus(string id)
        {
            var url = @$"transfers/{id}";

            HttpClient client = new HTTPClientHelper().Initialize(Helper.FlutterwaveSecretKey, Helper.FlutterwavBaseURL, _httpClientFactory);

            var response = await client.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            GetTransactionStatusReponse serviceResponse = JsonConvert.DeserializeObject<GetTransactionStatusReponse>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data.Amount = serviceResponse.Data.Amount / 100;

                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };            
            }
            return new ResponseModel { StatusCode = response.StatusCode, Msg = "Something went wrong, an error occured", Data = serviceResponse };
        }

        public async Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest)
        {
            var url = "transfers";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.FlutterwaveSecretKey, Helper.FlutterwavBaseURL, _httpClientFactory);

            transferRequest.TransactionReference = Helper.GenerateTransactionReference();

            var json = JsonConvert.SerializeObject(transferRequest);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _retryPolicy.ExecuteAsync(() => client.PostAsync(url, stringContent));

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var serviceResponse = JsonConvert.DeserializeObject<InitiateTransferResponse>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                if (serviceResponse.Data.Status.Equals("NEW"))
                {
                    await SaveTransaction(transferRequest, serviceResponse);
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
                        var retryResponse = await _retryPolicy.ExecuteAsync(() => client.PostAsync(url, stringContent));
                        var retryResponseContent = await retryResponse.Content.ReadAsStringAsync();
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

        public async Task<ResponseModel> ValidateAccount(ValidateAccountRequest validateAccount)
        {
            var url = @"accounts/resolve";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.FlutterwaveSecretKey, Helper.FlutterwavBaseURL, _httpClientFactory);

            var json = JsonConvert.SerializeObject(validateAccount);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var serviceResponse = JsonConvert.DeserializeObject<ValidateAccountResponse>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };
            }
            return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message };
        }

        private async Task SaveTransaction(MainTransferRequest transferRequest,
            InitiateTransferResponse serviceResponse)
        {
            TransactionHistory transactionHistory = new()
            {
                RecipientName = transferRequest.BeneficiaryAccountName,
                RecipientBank = transferRequest.BeneficiaryBankCodeFlutterwave,
                RecipientAcccountNumber = transferRequest.BeneficiaryAccountNumber,
                RecipientBankCode = transferRequest.BeneficiaryBankCodeFlutterwave,
                TransactionReference = serviceResponse.Data.Reference,
                TransactionStatus = serviceResponse.Data.Status,
                Currency = serviceResponse.Data.Currency,
                Source = transferRequest.source,
                Reason = transferRequest.Narration,
                DateCreated = serviceResponse.Data.CreatedAt,
                TransferCode = serviceResponse.Data.Id.ToString(),
                MaxRetryAttempt = transferRequest.MaxRetryAttempt,
                Provider = transferRequest.Provider,
            };

            await _transactionRepo.CreateAsync(transactionHistory);
            await _transactionRepo.Save();
        }
    }
}
