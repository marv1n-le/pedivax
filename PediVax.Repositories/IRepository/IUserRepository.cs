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
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task<(List<User> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize);
        Task<int> AddUser(User user);
        Task<int> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetUserByName(string keyword);
    }
}
