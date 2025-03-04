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
        Task<List<User>> GetAllUser(CancellationToken cancellationToken);
        Task<User> GetUserById(int id, CancellationToken cancellationToken);
        Task<(List<User> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<int> AddUser(User user, CancellationToken cancellationToken);
        Task<int> UpdateUser(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(int id, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<User>> GetUserByName(string keyword, CancellationToken cancellationToken);
    }
}
