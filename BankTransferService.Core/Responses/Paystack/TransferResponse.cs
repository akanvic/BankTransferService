using Newtonsoft.Json;

namespace BankTransferService.Core.Responses.Paystack
{
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
}
