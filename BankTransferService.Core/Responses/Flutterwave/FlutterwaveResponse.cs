using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankTransferService.Core.Responses
{
    public class FlutterwaveResponse
    {
    }
    public class ValidateAccountRequest
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_bank")]
        public string Code { get; set; }
        public string? Provider { get; set; }
    }
    public class ValidateAccountResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public ValidateAccountResponseData Data { get; set; }
    }
    public class ValidateAccountResponseData
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_name")]
        public string AccountName { get; set; }
    }

    public class InitiateTransferResponseData
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string FullName { get; set; }
        public string CreatedAt { get; set; }
        public string DebitCurrency { get; set; }
        public string CompleteMessage { get; set; }
        public byte RequiresApproval { get; set; }
        public byte IsApproved { get; set; }
        public string BankName { get; set; }
        public double Fee { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
    public class InitiateTransferResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public InitiateTransferResponseData? Data { get; set; }
    }
    public class InitiateTransferRequest
    {
        [JsonProperty(PropertyName = "account_number")]
        public string BeneficiaryAccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_bank")]
        public string BeneficiaryBankCode { get; set; }
        [JsonProperty(PropertyName = "debit_currency")]
        public string DebitCurrency { get; set; } = "NGN";

        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; } = "NGN";
        public int amount { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string TransactionReference { get; set; }
        [JsonProperty(PropertyName = "narration")]
        public string Narration { get; set; }
        public int? MaxRetryAttempt { get; set; }
        [JsonProperty(PropertyName = "callback_url")]
        public string? CallBackUrl { get; set; }
    }

    public class GetTransactionStatusReponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public GetTransactionStatusData Data { get; set; }
    }
    public class GetTransactionStatusData
    {
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string DebitCurrency { get; set; }
        public int Amount { get; set; }
        public double Fee { get; set; }
        public int Id { get; set; }
        public string Narration { get; set; }
        public string CompleteMessage { get; set; }
        public byte IsApproved { get; set; }
        public string BankName { get; set; }
        public byte RequiresApproval { get; set; }
        public string Approver { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string TransactionReference { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime TransactionDateTime { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string TransactionStatus { get; set; }
    }

    public class ListOfBankResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public List<ListOfBankData>? Data { get; set; }
    }
    public class ListOfBankData
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
    }
}
