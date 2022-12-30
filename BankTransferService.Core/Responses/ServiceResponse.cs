using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Core.Responses
{
    public class GenericTransactionStatusReponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public TransactionStatusData Data { get; set; }
    }
    public class TransactionStatusData
    {
        public RecipientTransactionStatusReponse Recipient { get; set; }
        public int Amount { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string TransactionReference { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime TransactionDateTime { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string TransactionStatus { get; set; }
    }
    public class RecipientTransactionStatusReponse
    {
        public RecipientTransactionDetails Details { get; set; }
    }
    public class RecipientTransactionDetails
    {
        [JsonProperty(PropertyName = "account_number")]
        public string BeneficiaryAccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_name")]
        public string BeneficiaryAccountName { get; set; }
        [JsonProperty(PropertyName = "bank_code")]
        public string BeneficiaryBankCode { get; set; }
        [JsonProperty(PropertyName = "bank_name")]
        public string BeneficiaryBankName { get; set; }
    }
    public class ServiceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<DataResponse>? Data { get; set; }
    }
    public class CheckBalanceResponse
    {
        public string Currency { get; set; }
        public int Balance { get; set; }
    }
    public class GenericCheckBalanceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<CheckBalanceResponse> Data { get; set; }
    }
    public class RecipientCreationResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public AccountValidationResponse? Data { get; set; }
    }
    public class RecipientCreationDetailsResponse
    {
        public string bank_code { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string account_name { get; set; }
    }
    public class AccountValidationResponse
    {
        public string recipient_code { get; set; }
        public RecipientCreationDetailsResponse Details { get; set; }
    }
    public class TransferGenericResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public TransferResponse? Data { get; set; }
    }
    public class TransferResponse
    {
        public string Reference { get; set; }
        public int Integration { get; set; }
        public string Domain { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Reason { get; set; }
        public int Recipient { get; set; }
        public string Status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string SessionId { get; set; }
        [JsonProperty(PropertyName = "transfer_code")]
        public string TransferCode { get; set; }
    }

    public class MainTransferRequest
    {
        public string Type { get; set; } = "nuban";
        [JsonProperty(PropertyName = "name")]
        public string BeneficiaryAccountName { get; set; }
        [JsonProperty(PropertyName = "account_number")]
        public string BeneficiaryAccountNumber { get; set; }
        [JsonProperty(PropertyName = "bank_code")]
        public string BeneficiaryBankCode { get; set; }
        [JsonProperty(PropertyName = "account_bank")]
        public string BeneficiaryBankCodeF { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; } = "NGN";

        public string source { get; set; } = "balance";
        public int amount { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string TransactionReference { get; set; }
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }
        public int? MaxRetryAttempt { get; set; }
        public string? recipient { get; set; }

        public string? Provider { get; set; }
        //Flutterwave

        public int otp { get; set; }
        [JsonProperty(PropertyName = "narration")]
        public string Narration { get; set; }
        [JsonProperty(PropertyName = "debit_currency")]
        public string DebitCurrency { get; set; } = "NGN";

        [JsonProperty(PropertyName = "callback_url")]
        public string? CallBackUrl { get; set; }
    }
    public class CreateReciepientRequest
    {
        public string Type { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string BeneficiaryAccountName { get; set; }
        [JsonProperty(PropertyName = "account_number")]
        public string BeneficiaryAccountNumber { get; set; }
        [JsonProperty(PropertyName = "bank_code")]
        public string BeneficiaryBankCode { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; }
    }
    public class TransferRequest
    {
        public string source { get; set; }
        public int amount { get; set; }
        public string reference { get; set; }
        public string recipient { get; set; }
        public string reason { get; set; }
    }
    //    "name": "Abbey Mortgage Bank",
    //"slug": "abbey-mortgage-bank",
    //"code": "801",
    //"longcode": "",
    //"gateway": null,
    //"pay_with_bank": false,
    //"active": true,
    //"is_deleted": false,
    //"country": "Nigeria",
    //"currency": "NGN",
    //"type": "nuban",
    //"id": 174,
    //"createdAt": "2020-12-07T16:19:09.000Z",
    //"updatedAt": "2020-12-07T16:19:19.000Z"
    public class DataResponse
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string LongCode { get; set; }
        public string Gateway { get; set; }
        public string PayWithBank { get; set; }
    }
}
