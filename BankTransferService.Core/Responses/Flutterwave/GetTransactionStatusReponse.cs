namespace BankTransferService.Core.Responses
{
    public class GetTransactionStatusReponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public GetTransactionStatusData Data { get; set; }
    }
}
