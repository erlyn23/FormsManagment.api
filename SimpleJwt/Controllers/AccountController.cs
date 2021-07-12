using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleJwt.Models.Requests;
using SimpleJwt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleJwt.Models.Responses;

namespace SimpleJwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountSvc;

        public AccountController(IAccountService accountSvc)
        {
            _accountSvc = accountSvc;
        }

        [HttpPost]
        public async Task<ActionResult<AccountRequest>> SaveUserAsync(AccountRequest accountRequest)
        {
            try
            {
                var saveUserResult = await _accountSvc.SaveUser(accountRequest);
                return CreatedAtRoute(new { userId = saveUserResult.UserId }, saveUserResult);
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { ExceptionMessage = $"{ex.Message}" }));
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AccountResponse>> LoginAsync(AuthRequest authRequest)
        {
            try
            {
                var response = await _accountSvc.Login(authRequest);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { ExceptionMessage = $"{ex.Message}" }));
            }
        }
    }
}
