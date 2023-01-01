using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack.Request
{
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
        public string BeneficiaryBankCodeFlutterwave { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string CurrencyCode { get; set; } = "NGN";

        public string source { get; set; } = "balance";
        public decimal amount { get; set; }
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
}
