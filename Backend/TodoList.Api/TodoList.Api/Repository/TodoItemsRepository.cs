using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoList.Api.Data;
using TodoList.Api.Model;
using TodoList.Api.Repository.IRepository;

namespace TodoList.Api.Repository
{
    public class TodoItemsRepository: Repository<TodoItem>,ITodoItemsRepository
    {
        private readonly ApplicationDataContext _db;

        public TodoItemsRepository(ApplicationDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task CreateTodoItem(TodoItem entity)
        {
            await CreateAsync(entity);
        }

        public async Task<List<TodoItem>> GetAllCompltedTodoItems()
        {
            return await GetAllAsync();
        }

        public async Task<TodoItem> GetTodoItem(Expression<Func<TodoItem, bool>> filter = null)
        {
            return await GetAsync(filter);
        }

        public async Task SaveTodoItem()
        {
            await SaveAsync();
        }

        public async Task<TodoItem> UpdateTodoItem(TodoItem entity)
        {
            _db.TodoItems.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
