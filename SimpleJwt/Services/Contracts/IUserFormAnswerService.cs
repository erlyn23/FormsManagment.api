using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IUserFormAnswerService
    {
        Task<bool> VerifyIfUserAnsweredFormAsync(int formId, int userId);
    }
}
