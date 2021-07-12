using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleJwt.Models.Requests;
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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("{questionId:int}")]
        public async Task<ActionResult<ResponseDto<List<AnswerDto>>>> GetAnswersAsync(int questionId)
        {
            var response = new ResponseDto<List<AnswerDto>>();
            try
            {
                response.Data = await _answerService.GetAnswersAsync(questionId);
                response.Status = 200;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }

            return Ok(response);
        }

        [HttpGet("GetAnswer/{answerId:int}", Name = "GetAnswer")]
        public async Task<ActionResult<ResponseDto<AnswerDto>>> GetAnswerAsync(int answerId)
        {
            var response = new ResponseDto<AnswerDto>();
            try
            {
                response.Data = await _answerService.GetAnswerAsync(answerId);
                response.Status = 200;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }

            return Ok(response);
        }

        [HttpPost("{userId:int}/{formId:int}")]
        public async Task<ActionResult<List<AnswerDto>>> AddAnswerAsync(AnswerDto[] answer, int userId, int formId)
        {
            try
            {
                var response = await _answerService.AddAnswerAsync(answer, userId, formId);
                if (response.Count > 0)
                    return Ok(response);

                return BadRequest(new { Status = 400, ErrorMessage = "Error al enviar respuesta" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 400, ErrorMessage = ex.InnerException.Message });
            }
        }
    }
}
