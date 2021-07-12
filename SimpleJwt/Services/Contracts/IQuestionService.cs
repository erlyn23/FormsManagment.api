using SimpleJwt.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> GetQuestionsAsync(int formId);
        Task<QuestionDto> GetQuestionAsync(int questionId);
        Task<QuestionDto> AddQuestionAsync(QuestionDto question);
        Task<QuestionDto> UpdateQuestionAsync(QuestionDto question);
        Task DeleteQuestionAsync(int questionId);
    }
}
