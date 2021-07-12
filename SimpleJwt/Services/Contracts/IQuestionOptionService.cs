using SimpleJwt.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IQuestionOptionService
    {
        Task<List<QuestionOptionDto>> GetQuestionOptionsAsync(int questionId);
        Task<QuestionOptionDto> GetQuestionOptionAsync(int questionOptionId);
        Task<QuestionOptionDto> AddQuestionOptionAsync(QuestionOptionDto questionOption);
        Task<QuestionOptionDto> UpdateQuestionOptionAsync(QuestionOptionDto questionOption);
        Task DeleteQuestionOptionAsync(int questionOptionId);
    }
}
