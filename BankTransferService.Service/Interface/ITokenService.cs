using BankTransferService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(BankUser user);
    }
}
