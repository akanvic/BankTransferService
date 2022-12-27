using BankTransferService.Core.DTOS;
using BankTransferService.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface IUserAuthService
    {
        Task<ResponseModel> Login(LoginDTO user);
    }   
}
