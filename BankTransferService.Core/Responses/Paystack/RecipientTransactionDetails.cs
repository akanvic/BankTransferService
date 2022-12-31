using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack
{
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
}
