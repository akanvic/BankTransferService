namespace BankTransferService.Core.Responses.Paystack
{
    public class ValidatePaystackAccountResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public ValidateAccountData? Data { get; set; }
    }
}
