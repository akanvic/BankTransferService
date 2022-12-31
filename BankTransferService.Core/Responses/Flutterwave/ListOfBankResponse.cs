namespace BankTransferService.Core.Responses
{
    public class ListOfBankResponse
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public List<ListOfBankData>? Data { get; set; }
    }
}
