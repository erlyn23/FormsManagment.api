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
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeService _questionTypeService;

        public QuestionTypeController(IQuestionTypeService questionTypeService)
        {
            _questionTypeService = questionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<QuestionTypeDto>>>> GetQuestionTypesAsync()
        {
            var response = new ResponseDto<List<QuestionTypeDto>>();
            try
            {
                response.Data = await _questionTypeService.GetQuestionTypesAsync();
                response.Status = 200;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpGet("{questionTypeId:int}")]
        public async Task<ActionResult<ResponseDto<QuestionTypeDto>>> GetQuestionTypeAsync(int questionTypeId)
        {
            var response = new ResponseDto<QuestionTypeDto>();
            try
            {
                var questionType = await _questionTypeService.GetQuestionTypeAsync(questionTypeId);

                if(questionType != null)
                {
                    response.Data = questionType;
                    response.Status = 200;
                }
                else
                {
                    response.Status = 404;
                    response.ErrorMessage = "Tipo de cuestionario no encontrado";
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }
    }
}
