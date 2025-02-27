using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
    }
}
