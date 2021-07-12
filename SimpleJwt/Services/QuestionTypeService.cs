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
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepository _questionTypeRepository;

        public QuestionTypeService(IQuestionTypeRepository questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }
        public async Task<QuestionTypeDto> GetQuestionTypeAsync(int questionTypeId)
        {
            try
            {
                var questionType = await _questionTypeRepository.GetOneAsync(q => q.QuestionTypeId == questionTypeId);

                var questionTypeDto = new QuestionTypeDto()
                {
                    QuestionTypeId = questionType.QuestionTypeId,
                    QuestionType = questionType.QuestionType1
                };
                return questionTypeDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<QuestionTypeDto>> GetQuestionTypesAsync()
        {
            try
            {
                var questionTypes = await _questionTypeRepository.GetAllAsync();
                questionTypes = questionTypes.OrderBy(q => q.QuestionTypeId).ToList();

                var questionTypesDto = new List<QuestionTypeDto>();
                foreach (var questionType in questionTypes)
                {
                    var questionTypeDto = await GetQuestionTypeAsync(questionType.QuestionTypeId);

                    questionTypesDto.Add(questionTypeDto);
                }
                return questionTypesDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
