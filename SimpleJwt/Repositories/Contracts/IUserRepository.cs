using SimpleJwt.Models.Entities;
using SimpleJwt.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Repositories.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
