using Newtonsoft.Json;


namespace BankTransferService.Core.Responses
{
    public class ValidateAccountResponseData
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_name")]
        public string AccountName { get; set; }
    }
}
