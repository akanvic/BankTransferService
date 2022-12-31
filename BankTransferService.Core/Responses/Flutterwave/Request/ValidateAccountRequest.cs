using Newtonsoft.Json;


namespace BankTransferService.Core.Responses.Flutterwave.Request
{
    public class ValidateAccountRequest
    {
        [JsonProperty(PropertyName = "account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty(PropertyName = "account_bank")]
        public string Code { get; set; }
        public string? Provider { get; set; }
    }
}
