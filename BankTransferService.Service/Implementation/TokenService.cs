using BankTransferService.Core.Entities;
using BankTransferService.Repo.Dapper.Infrastructure;
using BankTransferService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankTransferService.Service.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly ReadConfig config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService()
        {

        }
        public TokenService(IOptions<ReadConfig> options, IHttpContextAccessor httpContextAccessor,
            IConnectionFactory connectionfactory) : this()
        {
            config = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateAccessToken(BankUser user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var requestAt = DateTime.Now;
            var expiresIn = requestAt + TimeSpan.FromMinutes(int.Parse(config.TokenSpanMinutes));
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.EmailAddress, "TokenAuth"),
                new[] { new Claim("BankUserId", user.BankUserId.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("EmailAddress",user.EmailAddress),
                new Claim("FullName",$"{user.FirstName} {user.LastName}")});
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = config.Issuer,
                Audience = config.Audience1,
                SigningCredentials = signinCredentials,
                Subject = identity,
                Expires = expiresIn
            });

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
