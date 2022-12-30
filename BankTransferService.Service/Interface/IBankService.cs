using BankTransferService.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface IBankService
    {
        Task<ResponseModel> GetBankList();
        Task<ResponseModel> ValidateAccount(string accountNumber, string bankCode);
        Task<RecipientCreationResponse> CreateTransferReciepient(MainTransferRequest reciepientRequest);
        Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest);
        Task<ResponseModel> GetTransactionStatus(string reference);
    }
}
