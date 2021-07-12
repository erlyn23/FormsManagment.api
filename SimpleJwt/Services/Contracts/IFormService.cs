using SimpleJwt.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IFormService
    {
        Task<List<FormDto>> GetFormsAsync(int userId);
        Task<List<FormDto>> GetAllFormsAsync();
        Task<FormDto> GetFormAsync(int formId);
        Task<FormDto> AddFormAsync(FormDto form);
        Task<FormDto> UpdateFormAsync(FormDto form);
        Task DeleteFormAsync(int formId);
    }
}
