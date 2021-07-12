using SimpleJwt.Models.Entities;
using SimpleJwt.Models.Requests;
using SimpleJwt.Repositories.Contracts;
using SimpleJwt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;

        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }
        public async Task<FormDto> AddFormAsync(FormDto form)
        {
            try
            {
                form.Name = form.Name.Replace("etvcreate", "");
                var formEntity = new Form
                {
                    UserId = form.UserId,
                    Title = form.Title,
                    Name = form.Name,
                    Description = form.Description
                };

                await _formRepository.AddAsync(formEntity);
                await _formRepository.SaveChangesAsync();

                form.FormId = formEntity.FormId;
                return form;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteFormAsync(int formId)
        {
            try
            {
                var form = await _formRepository.GetOneAsync(f => f.FormId == formId);
                _formRepository.Remove(form);
                await _formRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<FormDto> GetFormAsync(int formId)
        {
            try
            {
                var form = await _formRepository.GetOneAsync(f => f.FormId == formId);
                var formDto = new FormDto();

                if (form != null)
                {
                    formDto.FormId = form.FormId;
                    formDto.UserId = form.UserId;
                    formDto.Name = form.Name;
                    formDto.Title = form.Title;
                    formDto.Description = form.Description;
                }
                   

                return formDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FormDto>> GetFormsAsync(int userId)
        {
            try
            {
                var forms = await _formRepository.GetManyWithFilterAsync(f => f.UserId == userId);

                var formsDto = new List<FormDto>();
                foreach(var form in forms)
                {
                    var formDto = await GetFormAsync(form.FormId);

                    formsDto.Add(formDto);
                }

                return formsDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<FormDto>> GetAllFormsAsync()
        {
            try
            {
                var forms = await _formRepository.GetAllAsync();

                var formsDto = new List<FormDto>();
                foreach (var form in forms)
                {
                    var formDto = await GetFormAsync(form.FormId);

                    formsDto.Add(formDto);
                }

                return formsDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<FormDto> UpdateFormAsync(FormDto form)
        {
            try
            {
                var formInDb = await _formRepository.GetOneAsync(f => f.FormId == form.FormId);
                formInDb.Title = form.Title;
                formInDb.Description = form.Description;

                await _formRepository.SaveChangesAsync();
                form.UserId = form.UserId;
                return form;
            }
            catch
            {
                throw;
            }
        }
    }
}
