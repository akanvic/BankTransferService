namespace BankTransferService.Core.Responses
{
    public class InitiateTransferResponseData
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DebitCurrency { get; set; }
        public string CompleteMessage { get; set; }
        public byte RequiresApproval { get; set; }
        public byte IsApproved { get; set; }
        public string BankName { get; set; }
        public double Fee { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}
