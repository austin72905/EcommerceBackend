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
        protected readonly EcommerceDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(EcommerceDBContext context)
        {
            _context = context;
            // 獲取指定實體類型 T 的 DbSet
            _dbSet = _context.Set<T>();
        }

        //可能為null
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
