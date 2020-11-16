using Projeto.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindByEmail(string email);
    }
}
