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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("{formId:int}")]
        public async Task<ActionResult<ResponseDto<List<QuestionDto>>>> GetFormsAsync(int formId)
        {
            var response = new ResponseDto<List<QuestionDto>>();
            try
            {
                response.Data = await _questionService.GetQuestionsAsync(formId);
                response.Status = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpGet("GetQuestion/{questionId:int}", Name = "GetQuestion")]
        public async Task<ActionResult<ResponseDto<QuestionDto>>> GetFormAsync(int questionId)
        {
            var response = new ResponseDto<QuestionDto>();
            try
            {
                var question = await _questionService.GetQuestionAsync(questionId);

                if (question != null)
                {
                    response.Data = question;
                    response.Status = 200;
                }
                else
                {
                    response.Status = 404;
                    response.ErrorMessage = "Formulario no existente";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<QuestionDto>>> AddFormAsync(QuestionDto questionDto)
        {
            var response = new ResponseDto<QuestionDto>();
            try
            {
                response.Data = await _questionService.AddQuestionAsync(questionDto);
                response.Status = 200;
                return CreatedAtRoute("GetQuestion", new { questionId = response.Data.QuestionId }, response.Data);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<QuestionDto>>> UpdateFormAsync(QuestionDto questionDto)
        {
            var response = new ResponseDto<QuestionDto>();
            try
            {
                response.Data = await _questionService.UpdateQuestionAsync(questionDto);
                response.Status = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpDelete("{questionId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<object>>> DeleteFormAsync(int questionId)
        {
            var response = new ResponseDto<object>();
            try
            {
                await _questionService.DeleteQuestionAsync(questionId);
                response.Data = new { Message = $"Pregunta eliminada correctamente" };
                response.Status = 200;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }
    }
}
