using DataSource.DBContext;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EcommerceDBContext _context;  // 寫入用（主庫）
        protected readonly EcommerceReadOnlyDBContext? _readContext;  // 讀取用（從庫，可選）
        protected readonly DbSet<T> _dbSet;  // 寫入用 DbSet
        protected readonly DbSet<T>? _readDbSet;  // 讀取用 DbSet（可選）

        public Repository(EcommerceDBContext context, EcommerceReadOnlyDBContext? readContext = null)
        {
            _context = context;
            _readContext = readContext;
            // 獲取指定實體類型 T 的 DbSet
            _dbSet = _context.Set<T>();
            _readDbSet = _readContext?.Set<T>();
        }

        /// <summary>
        /// 獲取讀取用的 DbContext（從庫），如果沒有則返回寫入用的 DbContext（主庫）
        /// 供子類別在複雜查詢中使用
        /// </summary>
        protected IEcommerceDbContext ReadContext => _readContext != null ? _readContext : _context;

        //可能為null
        public async Task<T?> GetByIdAsync(int id)
        {
            // 優先使用讀取 DbContext（從庫），如果沒有則使用寫入 DbContext（主庫）
            if (_readDbSet != null)
            {
                return await _readDbSet.FindAsync(id);
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // 優先使用讀取 DbContext（從庫），如果沒有則使用寫入 DbContext（主庫）
            if (_readDbSet != null)
            {
                return await _readDbSet.ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            // 寫入操作始終使用寫入 DbContext（主庫）
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            // 寫入操作始終使用寫入 DbContext（主庫）
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            // 寫入操作始終使用寫入 DbContext（主庫）
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            // 寫入操作始終使用寫入 DbContext（主庫）
            await _context.SaveChangesAsync();
        }
    }
}
