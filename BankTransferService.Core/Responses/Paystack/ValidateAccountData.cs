using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack
{
    public class ValidateAccountData
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_name")]
        public string AccountName { get; set; }
    }
}
