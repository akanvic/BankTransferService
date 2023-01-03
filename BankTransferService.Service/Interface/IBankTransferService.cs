using BankTransferService.Core.Responses;
using BankTransferService.Core.Responses.Flutterwave.Request;
using BankTransferService.Core.Responses.Paystack.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface IBankTransferService
    {
        Task<ResponseModel> GetBankList();
        Task<ResponseModel> ValidateAccount(ValidateAccountRequest validateAccount);
        Task<ResponseModel> InitiateTransfer(MainTransferRequest transferRequest);
        Task<ResponseModel> GetTransactionStatus(string reference);
    }
}
