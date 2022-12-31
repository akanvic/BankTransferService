namespace BankTransferService.Core.Responses.Paystack
{
    public class AccountValidationResponse
    {
        public string recipient_code { get; set; }
        public RecipientCreationDetailsResponse Details { get; set; }
    }
}
