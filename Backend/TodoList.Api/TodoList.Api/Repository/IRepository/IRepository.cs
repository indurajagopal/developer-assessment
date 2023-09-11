using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace TodoList.Api.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task SaveAsync();
    }
}
