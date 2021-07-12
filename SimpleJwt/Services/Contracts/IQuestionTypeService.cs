using SimpleJwt.Models.Entities;
using SimpleJwt.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IQuestionTypeService
    {
        Task<List<QuestionTypeDto>> GetQuestionTypesAsync();
        Task<QuestionTypeDto> GetQuestionTypeAsync(int questionTypeId);
    }
}
