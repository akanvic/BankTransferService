using BankTransferService.Core.DTOS;
using BankTransferService.Core.Responses;
using BankTransferService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankTransferService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _authService;

        public AuthController(IUserAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("UserLogin")]
        [SwaggerOperation(Summary = "User Login", Description = "User Login")]
        [SwaggerResponse(200, "Login Successful", typeof(ResponseModel))]
        public async Task<IActionResult> UserLogin([SwaggerParameter("The Login Parameter")][FromBody] LoginDTO login)
        {
            try
            {
                var user = await _authService.Login(login);
                if (user == null)
                {
                    return Unauthorized(user);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }

        }
    }
}
