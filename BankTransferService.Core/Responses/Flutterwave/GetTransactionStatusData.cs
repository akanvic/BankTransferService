using Newtonsoft.Json;


namespace BankTransferService.Core.Responses
{
    public class GetTransactionStatusData
    {
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string DebitCurrency { get; set; }
        public int Amount { get; set; }
        public double Fee { get; set; }
        public int Id { get; set; }
        public string Narration { get; set; }
        public string CompleteMessage { get; set; }
        public byte IsApproved { get; set; }
        public string BankName { get; set; }
        public byte RequiresApproval { get; set; }
        public string Approver { get; set; }
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
