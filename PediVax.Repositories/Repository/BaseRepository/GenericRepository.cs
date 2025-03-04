using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.Repositories.Repository.BaseRepository
{
    public class GenericRepository<T> where T : class
    {
        protected readonly PediVaxContext _context;
        public GenericRepository()
        {
            _context ??= new PediVaxContext();
        }
        public GenericRepository(PediVaxContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "IsActive") == 1)
                .ToListAsync(cancellationToken);
        }
        public async Task<(List<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            var query = _context.Set<T>().Where(e => EF.Property<int>(e, "IsActive") == 1);

            // Lấy tổng số bản ghi thoả mãn điều kiện IsActive = 1
            var totalRecords = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (data, totalRecords);
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var keyProperty = typeof(T).GetProperties()
                                  .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any())
                              ?? typeof(T).GetProperties().FirstOrDefault(p => p.Name.EndsWith("Id"));

            if (keyProperty == null)
            {
                throw new InvalidOperationException("Không thể xác định khóa chính của entity.");
            }

            // Lấy giá trị ID của entity
            var keyValue = keyProperty.GetValue(entity);
            if (keyValue == null)
            {
                throw new InvalidOperationException("Giá trị ID của entity không hợp lệ.");
            }

            // Tìm thực thể trong DbContext theo ID
            var existing = await _context.Set<T>().FindAsync(keyValue);
            if (existing != null)
            {
                _context.Entry(existing).State = EntityState.Detached; // Detach thực thể cũ
            }

            _context.Attach(entity); // Gắn entity mới vào context
            _context.Entry(entity).State = EntityState.Modified; // Đánh dấu để cập nhật

            return await _context.SaveChangesAsync();
        }


        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            // Lấy tên khóa chính thông qua reflection
            var keyProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()); // Tìm thuộc tính có annotation Key

            if (keyProperty == null)
            {
                // Nếu không tìm thấy annotation Key, sử dụng tên convention "Id"
                keyProperty = typeof(T).GetProperties().FirstOrDefault(p => p.Name == "Id");
            }

            if (keyProperty == null)
            {
                throw new InvalidOperationException("Không thể xác định khóa chính của entity.");
            }

            // Truy vấn dữ liệu từ entity dựa trên tên khóa chính
            var entity = await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "IsActive") == 1)  // Lọc theo IsActive = 1
                .FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name) == id, cancellationToken);  // Tìm theo khóa chính tự động xác định

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public T GetById(string code)
        {
            var entity = _context.Set<T>().Find(code);
            if (entity != null)
            {
            }

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }


        public async Task<T> GetByIdAsync(string code)
        {
            var entity = await _context.Set<T>().FindAsync(code);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public T GetById(Guid code)
        {
            var entity = _context.Set<T>().Find(code);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            var entity = await _context.Set<T>().FindAsync(code);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;

        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                var property = entity.GetType().GetProperty("IsActive");
                if (property != null)
                {
                    property.SetValue(entity, 0);
                    await UpdateAsync(entity, cancellationToken);
                    return true;
                }
            }
            return false;
        }

        public async Task<List<T>> GetByNameContainingAsync(string keyword, CancellationToken cancellationToken = default)
        {
            var propertyName = typeof(T).GetProperties()
                                   .FirstOrDefault(p => p.Name.Contains("Name"))?.Name;

            if (propertyName == null)
            {
                throw new InvalidOperationException("Cannot determine property name to search.");
            }

            return await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "IsActive") == 1)
                .Where(e => EF.Property<string>(e, propertyName).Contains(keyword))
                .ToListAsync(cancellationToken);
        }

        #region Separating asigned entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
