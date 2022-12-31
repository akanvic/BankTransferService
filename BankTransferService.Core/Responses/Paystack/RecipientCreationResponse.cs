namespace BankTransferService.Core.Responses.Paystack
{
    public class RecipientCreationResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public AccountValidationResponse? Data { get; set; }
    }
}
