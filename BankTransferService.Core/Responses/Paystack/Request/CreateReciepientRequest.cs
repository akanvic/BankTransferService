using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack.Request
{
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
}
