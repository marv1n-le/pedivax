using PediVax.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Services.IService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
