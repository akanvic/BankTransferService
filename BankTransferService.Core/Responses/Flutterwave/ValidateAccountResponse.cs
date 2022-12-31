namespace BankTransferService.Core.Responses
{
    public class ValidateAccountResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public ValidateAccountResponseData Data { get; set; }
    }
}
