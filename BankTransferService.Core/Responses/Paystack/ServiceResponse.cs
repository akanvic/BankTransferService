using BankTransferService.Core.Responses.Paystack;
using System;

namespace BankTransferService.Core.Responses
{
    public class ServiceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<DataResponse>? Data { get; set; }
    }
}
