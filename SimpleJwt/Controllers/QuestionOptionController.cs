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
    public class QuestionOptionController : ControllerBase
    {
        private readonly IQuestionOptionService _questionOptionService;

        public QuestionOptionController(IQuestionOptionService questionOptionService)
        {
            _questionOptionService = questionOptionService;
        }

        [HttpGet("{questionId:int}")]
        public async Task<ActionResult<ResponseDto<List<QuestionOptionDto>>>> GetQuestionOptionsAsync(int questionId)
        {
            var response = new ResponseDto<List<QuestionOptionDto>>();
            try
            {
                response.Data = await _questionOptionService.GetQuestionOptionsAsync(questionId);
                response.Status = 200;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }

            return Ok(response);
        }

        [HttpGet("GetQuestionOption/{questionOptionId}", Name = "GetQuestionOption")]
        public async Task<ActionResult<ResponseDto<QuestionOptionDto>>> GetQuestionOptionAsync(int questionOptionId)
        {
            var response = new ResponseDto<QuestionOptionDto>();
            try
            {
                var questionOption = await _questionOptionService.GetQuestionOptionAsync(questionOptionId);
                if(questionOption != null)
                {
                    response.Data = questionOption;
                    response.Status = 200;
                }
                else
                {
                    response.ErrorMessage = "Opción no encontrada";
                    response.Status = 404;
                }
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionOptionDto>> AddQuestionOptionAsync(QuestionOptionDto questionOption)
        {
            var response = new ResponseDto<QuestionOptionDto>();
            try
            {
                response.Data = await _questionOptionService.AddQuestionOptionAsync(questionOption);
                response.Status = 200;
                return CreatedAtRoute("GetQuestionOption", new { questionOptionId = response.Data.QuestionOptionId }, response.Data);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ResponseDto<QuestionOptionDto>>> UpdateQuestionOptionAsync(QuestionOptionDto questionOption)
        {
            var response = new ResponseDto<QuestionOptionDto>();
            try
            {
                response.Data = await _questionOptionService.UpdateQuestionOptionAsync(questionOption);
                response.Status = 200;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
            }
            return Ok(response);
        }

        [HttpDelete("{questionOptionId:int}")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteQuestionOptionAsync(int questionOptionId)
        {
            var response = new ResponseDto<string>();
            try
            {
                await _questionOptionService.DeleteQuestionOptionAsync(questionOptionId);
                response.Data = "Opción eliminada correctamente";
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
