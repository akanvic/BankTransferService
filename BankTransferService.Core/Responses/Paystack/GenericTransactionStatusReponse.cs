namespace BankTransferService.Core.Responses.Paystack
{
    public class GenericTransactionStatusReponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public TransactionStatusData Data { get; set; }
    }
}
