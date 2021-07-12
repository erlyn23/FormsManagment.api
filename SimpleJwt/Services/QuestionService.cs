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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IQuestionOptionService _questionOptionService;
        private readonly IAnswerService _answerService;

        public QuestionService(IQuestionRepository questionRepository, IQuestionTypeService questionTypeService, IQuestionOptionRepository questionOptionRepository, IQuestionOptionService questionOptionService, IAnswerService answerService)
        {
            _questionRepository = questionRepository;
            _questionTypeService = questionTypeService;
            _questionOptionRepository = questionOptionRepository;
            _questionOptionService = questionOptionService;
            _answerService = answerService;
        }

        public async Task<QuestionDto> AddQuestionAsync(QuestionDto question)
        {
            try
            {
                var questionEntity = new Question()
                {
                    QuestionTypeId = question.QuestionTypeId,
                    FormId = question.FormId,
                    Title = question.Title
                };

                await _questionRepository.AddAsync(questionEntity);
                await _questionRepository.SaveChangesAsync();

                question.QuestionId = questionEntity.QuestionId;
                var questionType = await _questionTypeService.GetQuestionTypeAsync(question.QuestionTypeId);
                question.QuestionType = questionType.QuestionType;
                return question;
            }
            catch
            {
                throw;
            }
        }

        public async Task<QuestionDto> GetQuestionAsync(int questionId)
        {
            try
            {
                var question = await _questionRepository.GetOneAsync(q => q.QuestionId == questionId);
                var questionType = await _questionTypeService.GetQuestionTypeAsync(question.QuestionTypeId);
                var questionOptions = await _questionOptionService.GetQuestionOptionsAsync(questionId);
                var answers = await _answerService.GetAnswersAsync(questionId);


                var questionDto = new QuestionDto();

                if (question != null)
                {
                    if(questionType != null)
                    {
                        questionDto.FormId = question.FormId;
                        questionDto.QuestionId = question.QuestionId;
                        questionDto.QuestionTypeId = question.QuestionTypeId;
                        questionDto.QuestionType = questionType.QuestionType;
                        questionDto.Title = question.Title;
                        questionDto.QuestionOptions = questionOptions;
                        questionDto.Answers = answers;
                    }
                    else
                    {
                        throw new Exception("El tipo de pregunta no es válido");
                    }
                }

                return questionDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<QuestionDto>> GetQuestionsAsync(int formId)
        {
            try
            {
                var questions = await _questionRepository.GetManyWithFilterAsync(q => q.FormId == formId);

                var questionsDto = new List<QuestionDto>();
                
                foreach(var question in questions)
                {
                    var questionType = await _questionTypeService.GetQuestionTypeAsync(question.QuestionTypeId);
                    var questionDto = await GetQuestionAsync(question.QuestionId);
                    questionsDto.Add(questionDto);
                }

                return questionsDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<QuestionDto> UpdateQuestionAsync(QuestionDto question)
        {
            try
            {
                var questionInDb = await _questionRepository.GetOneAsync(q => q.QuestionId == question.QuestionId);

                if (questionInDb.QuestionTypeId != question.QuestionTypeId)
                    await DeleteQuestionOptionsAsync(question.QuestionId);

                questionInDb.QuestionTypeId = question.QuestionTypeId;
                questionInDb.Title = question.Title;

                await _questionRepository.SaveChangesAsync();

                var questionType = await _questionTypeService.GetQuestionTypeAsync(question.QuestionTypeId);
                question.QuestionType = questionType.QuestionType;
                return question;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            try
            {
                var question = await _questionRepository.GetOneAsync(q => q.QuestionId == questionId);
                _questionRepository.Remove(question);
                await DeleteQuestionOptionsAsync(questionId);
                await _questionRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task DeleteQuestionOptionsAsync(int questionId)
        {
            try
            {
                var questionOptions = await _questionOptionRepository.GetManyWithFilterAsync(q => q.QuestionId == questionId);

                _questionOptionRepository.RemoveRange(questionOptions.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }
}
