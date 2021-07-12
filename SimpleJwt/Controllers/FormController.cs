using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<FormDto>>>> GetAllFormsAsync()
        {
            var response = new ResponseDto<List<FormDto>>();
            try
            {
                response.Data = await _formService.GetAllFormsAsync();
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

        [HttpGet("{userId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<List<FormDto>>>> GetFormsAsync(int userId)
        {
            var response = new ResponseDto<List<FormDto>>();
            try
            {
                response.Data = await _formService.GetFormsAsync(userId);
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

        [HttpGet("GetForm/{formId:int}", Name = "GetForm")]
        public async Task<ActionResult<ResponseDto<FormDto>>> GetFormAsync(int formId)
        {
            var response = new ResponseDto<FormDto>();
            try
            {
                response.Data = await _formService.GetFormAsync(formId);
                if (response.Data.FormId > 0)
                    response.Status = 200;
                else
                {
                    response.Status = 404;
                    response.ErrorMessage = "Formulario no existente";
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<FormDto>>> AddFormAsync(FormDto formDto)
        {
            var response = new ResponseDto<FormDto>();
            try
            {
                response.Data = await _formService.AddFormAsync(formDto);
                response.Status = 200;
                return CreatedAtRoute("GetForm", new { formId = response.Data.FormId }, response.Data);
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = 400;
                return Ok(response);
            }
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<FormDto>>> UpdateFormAsync(FormDto formDto)
        {
            var response = new ResponseDto<FormDto>();
            try
            {
                response.Data = await _formService.UpdateFormAsync(formDto);
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

        [HttpDelete("{formId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseDto<object>>> DeleteFormAsync(int formId)
        {
            var response = new ResponseDto<object>();
            try
            {
                await _formService.DeleteFormAsync(formId);
                response.Data = new { Message = $"Formulario eliminado correctamente" };
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

    }
}
