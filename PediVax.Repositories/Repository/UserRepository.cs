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
using PediVax.BusinessObjects.Enum;

namespace PediVax.Repositories.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository() : base()
        {
        }

        public async Task<List<User>> GetAllUser(CancellationToken cancellationToken)
        {
            return await GetAllAsync(cancellationToken);
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.Include(u => u.ChildProfiles)
                .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
        }

        public async Task<(List<User> Data, int TotalCount)> GetUserPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<int> AddUser(User user, CancellationToken cancellationToken)
        {
            return await CreateAsync(user, cancellationToken);
        }

        public async Task<int> UpdateUser(User user, CancellationToken cancellationToken)
        {
            return await UpdateAsync(user, cancellationToken);
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            return await DeleteAsync(id, cancellationToken);
        }
        
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.IsActive == EnumList.IsActive.Active)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<List<User>> GetUserByName(string keyword, CancellationToken cancellationToken)
        {
            return await GetByNameContainingAsync(keyword);
        }
    }
}
