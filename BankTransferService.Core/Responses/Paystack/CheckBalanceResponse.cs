namespace BankTransferService.Core.Responses.Paystack
{
    public class CheckBalanceResponse
    {
        public string Currency { get; set; }
        public int Balance { get; set; }
    }
}
