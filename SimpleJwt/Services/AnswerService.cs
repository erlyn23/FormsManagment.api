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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserFormAnswerRepository _userFormAnswerRepository;

        public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository, IUserFormAnswerRepository userFormAnswerRepository)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
             _userFormAnswerRepository = userFormAnswerRepository;
        }
        public async Task<List<AnswerDto>> AddAnswerAsync(AnswerDto[] answers, int userId, int formId)
        {
            try
            {
                var answerEntities = new List<Answer>();

                foreach (var answerDto in answers)
                {
                    var answerEntity = new Answer()
                    {
                        QuestionId = answerDto.QuestionId,
                        Answer1 = answerDto.Answer
                    };
                    answerEntities.Add(answerEntity);
                }

                var savedAnswers = await _answerRepository.AddRangeAsync(answerEntities.ToArray());
                await _userFormAnswerRepository.AddAsync(new UserFormAnswer()
                {
                    UserId = userId,
                    FormId = formId
                });
                await _answerRepository.SaveChangesAsync();
                for (int i = 0; i < savedAnswers.Count; i++)
                {
                    answers[i].AnswerId = savedAnswers[i].AnswerId;
                }

                return answers.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<AnswerDto> GetAnswerAsync(int answerId)
        {
            try
            {
                var answer = await _answerRepository.GetOneAsync(a => a.AnswerId == answerId);

                if(answer != null)
                {
                    var question = await _questionRepository.GetOneAsync(q => q.QuestionId == answer.QuestionId);
                    AnswerDto answerResponse = new AnswerDto()
                    {
                        AnswerId = answer.AnswerId,
                        Answer = answer.Answer1,
                        QuestionId = answer.QuestionId,
                        QuestionTitle = question.Title
                    };

                    return answerResponse;
                }
                throw new Exception("Respuesta no encontrada");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<AnswerDto>> GetAnswersAsync(int questionId)
        {
            try
            {
                var answers = await _answerRepository.GetManyWithFilterAsync(a => a.QuestionId == questionId);

                var answersDto = new List<AnswerDto>();
                foreach(var answer in answers)
                {
                    var answerDto = await GetAnswerAsync(answer.AnswerId);
                    answersDto.Add(answerDto);
                }

                return answersDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
