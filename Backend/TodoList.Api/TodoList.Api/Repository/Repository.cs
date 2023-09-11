using TodoList.Api.Repository.IRepository;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoList.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TodoList.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDataContext _db;
        private DbSet<T> dbSet;
        public Repository(ApplicationDataContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = dbSet;
            query = query.AsNoTracking();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
