namespace BankTransferService.Core.Responses
{
    public class GetTransactionStatusReponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public GetTransactionStatusData Data { get; set; }
    }
}
