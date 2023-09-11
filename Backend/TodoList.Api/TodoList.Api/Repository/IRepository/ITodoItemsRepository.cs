using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using TodoList.Api.Model;

namespace TodoList.Api.Repository.IRepository
{
    public interface ITodoItemsRepository: IRepository<TodoItem>
    {
        Task<TodoItem> UpdateTodoItem(TodoItem entity);
        Task<List<TodoItem>> GetAllCompltedTodoItems();
        Task<TodoItem> GetTodoItem(Expression<Func<TodoItem, bool>> filter = null);
        Task CreateTodoItem(TodoItem entity);
        Task SaveTodoItem();
    }
}
