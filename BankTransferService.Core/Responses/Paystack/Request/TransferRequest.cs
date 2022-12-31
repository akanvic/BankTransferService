namespace BankTransferService.Core.Responses.Paystack.Request
{
    public class TransferRequest
    {
        public string source { get; set; }
        public int amount { get; set; }
        public string reference { get; set; }
        public string recipient { get; set; }
        public string reason { get; set; }
    }
}
