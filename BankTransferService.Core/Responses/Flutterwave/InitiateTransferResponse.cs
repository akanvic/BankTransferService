namespace BankTransferService.Core.Responses
{
    public class InitiateTransferResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public InitiateTransferResponseData? Data { get; set; }
    }
}
