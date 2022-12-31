namespace BankTransferService.Core.Responses.Paystack
{
    public class RecipientCreationDetailsResponse
    {
        public string bank_code { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string account_name { get; set; }
    }
}
