using SimpleJwt.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IAnswerService 
    {
        Task<List<AnswerDto>> GetAnswersAsync(int questionId);
        Task<AnswerDto> GetAnswerAsync(int answerId);
        Task<List<AnswerDto>> AddAnswerAsync(AnswerDto[] answer, int userId, int formId);
    }
}
