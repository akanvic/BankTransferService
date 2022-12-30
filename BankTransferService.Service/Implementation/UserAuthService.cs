using BankTransferService.Core.DTOS;
using BankTransferService.Core.Entities;
using BankTransferService.Core.Responses;
using BankTransferService.Repo.Dapper.Implementation;
using BankTransferService.Repo.Dapper.Infrastructure;
using BankTransferService.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BankTransferService.Repo.Dapper.Infrastructure.Connectionfactory;

namespace BankTransferService.Service.Implementation
{
    public class UserAuthService : IUserAuthService
    {
        private readonly GenericRepository<BankUser> _userrepo;
        private readonly ITokenService _tokenService;

        public UserAuthService(ITokenService tokenService, IConnectionFactory connectionfactory)
        {
            _userrepo = new GenericRepository<BankUser>(connectionfactory);
            _tokenService = tokenService;
        }

        public async Task<ResponseModel> Login(LoginDTO user)
        {
            var response = await _userrepo.QueryFirstOrDefaultAsyncSp(StoredProcedures.uspAddTraining, CommandType.StoredProcedure ,
                new { EmailAddress = user.UserName, Password = user.Password});

            if (response == null)
                return (new ResponseModel { Msg = "Invalid Login Details", StatusCode= HttpStatusCode.BadRequest });

            var token = _tokenService.GenerateAccessToken(response);
            return (new ResponseModel { Data = token, Msg = "Login Successful", StatusCode = HttpStatusCode.BadRequest });
        }
    }
}
