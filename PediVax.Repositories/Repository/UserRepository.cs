using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using PediVax.BusinessObjects.Models;
using PediVax.Repositories.IRepository;
using PediVax.Repositories.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository() : base()
        {
        }

        public async Task<List<User>> GetAllUser()
        {
            return await GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetByIdAsync(id) ;
        }

        public async Task<(List<User> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize)
        {
            return await GetPagedAsync(pageNumber, pageSize);
        }

        public async Task<int> AddUser(User user)
        {
            return await CreateAsync(user);
        }

        public async Task<int> UpdateUser(User user)
        {
            return await UpdateAsync(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await DeleteAsync(id);
        }
    }
}
