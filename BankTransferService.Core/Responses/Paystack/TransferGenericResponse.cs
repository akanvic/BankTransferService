namespace BankTransferService.Core.Responses.Paystack
{
    public class TransferGenericResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public TransferResponse? Data { get; set; }
    }
}
