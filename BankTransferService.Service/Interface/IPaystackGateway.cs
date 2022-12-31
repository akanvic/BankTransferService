using BankTransferService.Core.Responses;
using BankTransferService.Core.Responses.Paystack;
using BankTransferService.Core.Responses.Paystack.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface IPaystackGateway
    {
        Task<ResponseModel> GetBankList();
        Task<ResponseModel> ValidateAccount(string accountNumber, string bankCode);
        Task<RecipientCreationResponse> CreateTransferReciepient(MainTransferRequest reciepientRequest);
        Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest);
        Task<ResponseModel> GetTransactionStatus(string reference);
    }
}
