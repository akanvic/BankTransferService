using Newtonsoft.Json;


namespace BankTransferService.Core.Responses.Flutterwave.Request
{
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
}
