using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleJwt.Models.Responses;
using SimpleJwt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserFormAnswerController : ControllerBase
    {
        private readonly IUserFormAnswerService _userFormAnswerService;

        public UserFormAnswerController(IUserFormAnswerService userFormAnswerService)
        {
            _userFormAnswerService = userFormAnswerService;
        }

        [HttpGet("{formId:int}/{userId:int}")]
        public async Task<ActionResult<ResponseDto<string>>> VerifyIfUserAnsweredForm(int formId, int userId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var isFormAnswered = await _userFormAnswerService.VerifyIfUserAnsweredFormAsync(formId, userId);
                response.Data = isFormAnswered.ToString();
                response.Status = 200;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }

            return Ok(response);
        }
    }
}
