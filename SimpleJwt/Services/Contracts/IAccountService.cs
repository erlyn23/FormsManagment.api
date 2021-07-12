using SimpleJwt.Models.Requests;
using SimpleJwt.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Services.Contracts
{
    public interface IAccountService
    {
        Task<AccountRequest> SaveUser(AccountRequest accountRequest);
        Task<AccountResponse> Login(AuthRequest authRequest);
    }
}
