using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack
{
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
}
