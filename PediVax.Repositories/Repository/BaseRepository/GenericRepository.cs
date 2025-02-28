using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "IsActive") == 1)
                .ToListAsync();
        }
        public async Task<(List<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            var query = _context.Set<T>().Where(e => EF.Property<int>(e, "IsActive") == 1);

            // Lấy tổng số bản ghi thoả mãn điều kiện IsActive = 1
            var totalRecords = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalRecords);
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();

            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    var tracker = _context.Attach(entity);
            //    tracker.State = EntityState.Modified;
            //}
            //_context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            //var trackerEntity = _context.Set<T>().Local.FirstOrDefault(e => e == entity);
            //if (trackerEntity != null)
            //{
            //    _context.Entry(trackerEntity).State = EntityState.Detached;
            //}
            //var tracker = _context.Attach(entity);
            //tracker.State = EntityState.Modified;
            //return await _context.SaveChangesAsync();

            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();

            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    var tracker = _context.Attach(entity);
            //    tracker.State = EntityState.Modified;
            //}

            //return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
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

        //public async Task<T> GetByIdAsync(int id, string idPropertyName)
        //{
        //    var entity = await _context.Set<T>()
        //        .Where(e => EF.Property<int>(e, "IsActive") == 1)
        //        .FirstOrDefaultAsync(e => EF.Property<int>(e, idPropertyName) == id);
        //    if (entity != null)
        //    {
        //        _context.Entry(entity).State = EntityState.Detached;
        //    }

        //    return entity;
        //}

        public async Task<T> GetByIdAsync(int id)
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
                .FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name) == id);  // Tìm theo khóa chính tự động xác định

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

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                var property = entity.GetType().GetProperty("IsActive");
                if (property != null)
                {
                    property.SetValue(entity, 0);
                    await UpdateAsync(entity);
                    return true;
                }
            }
            return false;
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
