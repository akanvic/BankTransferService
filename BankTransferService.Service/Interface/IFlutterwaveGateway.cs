using BankTransferService.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface IFlutterwaveGateway
    {
        Task<ResponseModel> GetBankList();
        Task<ResponseModel> ValidateAccount(ValidateAccountRequest validateAccount);
        Task<RecipientCreationResponse> CreateTransferReciepient(MainTransferRequest reciepientRequest);
        Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest);
        Task<ResponseModel> GetTransactionStatus(string reference);
    }
}
