using BankTransferService.Core.Responses;
using BankTransferService.Core.Responses.Flutterwave.Request;
using BankTransferService.Core.Responses.Paystack.Request;
using BankTransferService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankTransferService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IPaystackGateway _bankService;
        private readonly IFlutterwaveGateway _flutterwaveGateway;

        public BankController(IPaystackGateway bankService, IFlutterwaveGateway flutterwaveGateway)
        {
            _bankService = bankService;
            _flutterwaveGateway = flutterwaveGateway;
        }

        [HttpGet("banks")]
        public async Task<IActionResult> GetBankList(string provider)
        {
            try
            {
                ResponseModel response = null;
                if(provider.Equals("paystack"))
                    response = await _bankService.GetBankList();

                else if (provider.Equals("flutterwave"))
                    response = await _flutterwaveGateway.GetBankList();
                else
                    response = await _bankService.GetBankList();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpPost("ValidateAccount")]
        public async Task<IActionResult> ValidateAccount(ValidateAccountRequest validateAccount)
        {
            try
            {
                ResponseModel response = null;
                if (validateAccount.Provider.Equals("paystack"))
                    response = await _bankService.ValidateAccount(validateAccount.AccountNumber, validateAccount.Code);

                else if (validateAccount.Provider.Equals("flutterwave"))
                     response = await _flutterwaveGateway.ValidateAccount(validateAccount);
                else
                    response = await _bankService.ValidateAccount(validateAccount.AccountNumber, validateAccount.AccountNumber);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpPost("InitiateTransfer")]
        public async Task<IActionResult> InitiateTransfer(MainTransferRequest transferRequest)
        {
            try
            {
                ResponseModel response = null;
                if (transferRequest.Provider.Equals("paystack"))
                    response = await _bankService.InitiateTransfer(transferRequest);

                else if (transferRequest.Provider.Equals("flutterwave"))
                    response = await _flutterwaveGateway.InitiateTransfer(transferRequest);
                else
                    response = await _bankService.InitiateTransfer(transferRequest);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }

        [HttpGet("GetTransactionStatus")]
        public async Task<IActionResult> GetTransactionStatus(string reference)
        {
            try
            {
                var response = await _bankService.GetTransactionStatus(reference);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }
    }
}
