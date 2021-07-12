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
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly IQuestionOptionRepository _questionOptionRepository;

        public QuestionOptionService(IQuestionOptionRepository questionOptionRepository)
        {
            _questionOptionRepository = questionOptionRepository;
        }
        public async Task<QuestionOptionDto> AddQuestionOptionAsync(QuestionOptionDto questionOption)
        {
            try
            {
                QuestionOption questionEntity = new QuestionOption()
                {
                    QuestionId = questionOption.QuestionId,
                    Title = questionOption.Title
                };

                await _questionOptionRepository.AddAsync(questionEntity);
                await _questionOptionRepository.SaveChangesAsync();
                questionOption.QuestionOptionId = questionEntity.QuestionOptionId;
                return questionOption;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteQuestionOptionAsync(int questionOptionId)
        {
            try
            {
                var questionOption = await _questionOptionRepository.GetOneAsync(q => q.QuestionOptionId == questionOptionId);
                _questionOptionRepository.Remove(questionOption);
                await _questionOptionRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<QuestionOptionDto> GetQuestionOptionAsync(int questionOptionId)
        {
            try
            {
                var questionOption = await _questionOptionRepository.GetOneAsync(q => q.QuestionOptionId == questionOptionId);

                var questionOptionDto = new QuestionOptionDto()
                {
                    QuestionOptionId = questionOption.QuestionOptionId,
                    QuestionId = questionOption.QuestionId,
                    Title = questionOption.Title
                };

                return questionOptionDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<QuestionOptionDto>> GetQuestionOptionsAsync(int questionId)
        {
            try
            {
                var questionOptions = await _questionOptionRepository.GetManyWithFilterAsync(q => q.QuestionId == questionId);

                var questionOptionsDto = new List<QuestionOptionDto>();
                foreach(var questionOption in questionOptions)
                {
                    var questionOptionDto = await GetQuestionOptionAsync(questionOption.QuestionOptionId);
                    questionOptionsDto.Add(questionOptionDto);
                }

                return questionOptionsDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<QuestionOptionDto> UpdateQuestionOptionAsync(QuestionOptionDto questionOption)
        {
            try
            {
                var questionOptionInDb = 
                    await _questionOptionRepository.GetOneAsync(q => q.QuestionOptionId == questionOption.QuestionOptionId);

                questionOptionInDb.Title = questionOption.Title;

                await _questionOptionRepository.SaveChangesAsync();
                return questionOption;

            }
            catch
            {
                throw;
            }
        }
    }
}
