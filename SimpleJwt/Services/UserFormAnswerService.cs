using SimpleJwt.Repositories.Contracts;
using SimpleJwt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services
{
    public class UserFormAnswerService : IUserFormAnswerService
    {
        private readonly IUserFormAnswerRepository _userFormAnswerRepository;

        public UserFormAnswerService(IUserFormAnswerRepository userFormAnswerRepository)
        {
            _userFormAnswerRepository = userFormAnswerRepository;
        }
        public async Task<bool> VerifyIfUserAnsweredFormAsync(int formId, int userId)
        {
            try
            {
                var answeredForm = await _userFormAnswerRepository.GetOneAsync(ua => ua.UserId == userId && ua.FormId == formId);
                if (answeredForm != null)
                    return true;

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
