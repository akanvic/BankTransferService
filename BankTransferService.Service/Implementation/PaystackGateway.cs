using BankTransferService.Core.Entities;
using BankTransferService.Core.Responses;
using BankTransferService.Core.Responses.Paystack;
using BankTransferService.Core.Responses.Paystack.Request;
using BankTransferService.Repo.Data.Repository.Interfaces;
using BankTransferService.Service.Interface;
using BankTransferService.Service.Utilities;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Implementation
{
    public class PaystackGateway : IPaystackGateway
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        public PaystackGateway(ITransactionRepo transactionRepo, IHttpClientFactory httpClientFactory)
        {
            _transactionRepo = transactionRepo;
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<ResponseModel> GetBankList()
        {
            var url = "bank?currency=NGN";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);
            
            var response = await client.GetAsync(url);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(responseContent);

            if (response.IsSuccessStatusCode)
                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };
            
            return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message};
        }

        public async Task<ResponseModel> ValidateAccount(string accountNumber, string bankCode)
        {
            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);
            var url = @$"bank/resolve?account_number={accountNumber}&bank_code={bankCode}";

            var response = await client.GetAsync(url);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var serviceResponse = JsonConvert.DeserializeObject<ValidatePaystackAccountResponse>(responseContent);
            
            if (response.IsSuccessStatusCode)
                return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message, Data = serviceResponse };
            return new ResponseModel { StatusCode = response.StatusCode, Msg = serviceResponse.Message };
        }

        public async Task<RecipientCreationResponse> CreateTransferReciepient(MainTransferRequest reciepientRequest)
        {
            string url = @"transferrecipient";
            RecipientCreationResponse serviceResponse = null;

            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);
            
            var json = JsonConvert.SerializeObject(reciepientRequest);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(url, stringContent);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            serviceResponse = JsonConvert.DeserializeObject<RecipientCreationResponse>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                return serviceResponse;
            }
            return serviceResponse;
        }

        public async Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest)
        {
            var recipientResponse = CreateTransferReciepient(transferRequest).Result;
            if(recipientResponse.Data is null)
                return new ResponseModel { StatusCode = HttpStatusCode.BadRequest, Msg = recipientResponse.Message };

            var checkBalanceResponse = CheckBalance().Result;
            var balanceInNaira = checkBalanceResponse.Data.FirstOrDefault().Balance / 100;

            if(balanceInNaira < transferRequest.amount)
                return new ResponseModel { StatusCode = HttpStatusCode.BadRequest, Msg = "Insufficient Balance",Data=checkBalanceResponse };

            var url = @"transfer";

            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);

            transferRequest.TransactionReference = Helper.GenerateTransactionReference();
            transferRequest.amount = transferRequest.amount * 100;
            transferRequest.recipient = recipientResponse.Data.recipient_code;

            var json = JsonConvert.SerializeObject(transferRequest);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            var serviceResponse = JsonConvert.DeserializeObject<TransferGenericResponse>(responseContent);
            
            if (response.IsSuccessStatusCode)
             {
                 if (serviceResponse.Data.Status.Equals("success"))
                 {
                     await SaveTransaction(transferRequest, serviceResponse, recipientResponse);
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
                         var retryResponse = await client.PostAsync(new Uri(url), stringContent);
                         var retryResponseContent = await retryResponse.Content.ReadAsStringAsync();
                         var retryServiceResponse = JsonConvert.DeserializeObject<TransferGenericResponse>(retryResponseContent);

                         if (retryServiceResponse.Data.Status.Equals("success"))
                         {
                             // If the request was successful, return true
                             await SaveTransaction(transferRequest, serviceResponse, recipientResponse);
                             return new ResponseModel { StatusCode = response.StatusCode, Msg = retryServiceResponse.Message, Data = serviceResponse }; ;
                         }

                         retries++;
                     }

                     // If all retries have failed, return false
                     return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer was not successful", Data=serviceResponse }; ;
                 }
                 
             }
             return new ResponseModel { StatusCode = response.StatusCode, Msg = "An Erro Occured Transfer was not successful" };
            
        }

        private async Task<GenericCheckBalanceResponse> CheckBalance()
        {
            var url = @"balance";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);

            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            GenericCheckBalanceResponse serviceResponse = JsonConvert.DeserializeObject<GenericCheckBalanceResponse>(responseContent);
            if (response.IsSuccessStatusCode)
            {
                return serviceResponse;
            }
            return serviceResponse;
        }

        public async Task<ResponseModel> GetTransactionStatus(string reference)
        {
            var url = @$"transfer/verify/{reference}";
            HttpClient client = new HTTPClientHelper().Initialize(Helper.PaystackSecretKey, Helper.PaystackBaseURL, _httpClientFactory);

            var response = await client.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            GenericTransactionStatusReponse serviceResponse = JsonConvert.DeserializeObject<GenericTransactionStatusReponse>(responseContent);
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data.Amount = serviceResponse.Data.Amount / 100;

                if (serviceResponse.Data.TransactionStatus.Equals("success"))
                    return new ResponseModel { StatusCode=response.StatusCode, Msg="Transfer was successful", Data=serviceResponse};
                else if(serviceResponse.Data.TransactionStatus.Equals("pending"))
                    return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer is pending", Data = serviceResponse };
                else if (serviceResponse.Data.TransactionStatus.Equals("failed"))
                    return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer failed", Data = serviceResponse };
                else if (serviceResponse.Data.TransactionStatus.Equals("reversed"))
                    return new ResponseModel { StatusCode = response.StatusCode, Msg = "Transfer reversed", Data = serviceResponse };
            }
            return new ResponseModel { StatusCode = response.StatusCode, Msg = "Something went wrong, an error occured", Data = serviceResponse };
        }
        private async Task SaveTransaction(MainTransferRequest transferRequest, 
            TransferGenericResponse serviceResponse, RecipientCreationResponse recipientResponse)
        {
            TransactionHistory transactionHistory = new TransactionHistory()
            {
                RecipientName = transferRequest.BeneficiaryAccountName,
                RecipientBank = recipientResponse.Data.Details.bank_name,
                RecipientAcccountNumber = recipientResponse.Data.Details.account_number,
                RecipientBankCode = recipientResponse.Data.Details.bank_code,
                TransactionReference = serviceResponse.Data.Reference,
                TransactionStatus = serviceResponse.Data.Status,
                Currency = serviceResponse.Data.Currency,
                Source = transferRequest.source,
                Reason = serviceResponse.Data.Reason,
                DateCreated = serviceResponse.Data.createdAt,
                DateUpdated = serviceResponse.Data.updatedAt,
                SessionId = serviceResponse.Data.SessionId,
                TransferCode = serviceResponse.Data.TransferCode,
                MaxRetryAttempt = transferRequest.MaxRetryAttempt
            };

            await _transactionRepo.CreateAsync(transactionHistory);
            await _transactionRepo.Save();
        }
        
    }
}
