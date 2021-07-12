using SimpleJwt.DbContexts;
using SimpleJwt.Models.Entities;
using SimpleJwt.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(FormsManagmentDbContext dbContext) : base(dbContext)
        {

        }
    }
}
