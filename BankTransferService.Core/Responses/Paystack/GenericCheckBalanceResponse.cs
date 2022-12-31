namespace BankTransferService.Core.Responses.Paystack
{
    public class GenericCheckBalanceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<CheckBalanceResponse> Data { get; set; }
    }
}
