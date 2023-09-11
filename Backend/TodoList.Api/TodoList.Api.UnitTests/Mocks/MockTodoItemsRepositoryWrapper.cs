using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.Api.Model;
using TodoList.Api.Repository.IRepository;

namespace TodoList.Api.UnitTests.Mocks
{
    internal class MockTodoItemsRepositoryWrapper
    {
        
        public static Mock<ITodoItemsRepository> GetMock()
        {
            var mock = new Mock<ITodoItemsRepository>();

            var itemData = new List<TodoItem>
            {
                new TodoItem
                {
                    Id=new Guid("d409e407-cf63-4468-8189-75b0956ac13a"),
                    Description="Test1",
                    IsCompleted=false
                },
                new TodoItem
                {
                    Id=new Guid("29b0c44d-66cd-4801-ac59-b0007c1f883e"),
                    Description="Test2",
                    IsCompleted=false
                },
                new TodoItem
                {
                    Id=new Guid("143b7f31-3ecc-4204-9594-230ed0a90d8c"),
                    Description="Test3",
                    IsCompleted=true
                }
            };
            mock.Setup(m => m.GetAllCompltedTodoItems()).ReturnsAsync(() => itemData);
            mock.Setup(m => m.GetTodoItem(It.IsAny<Expression<Func<TodoItem, bool>>>()))
            .ReturnsAsync((Guid id) => itemData.FirstOrDefault(o => o.Id == id));
            mock.Setup(m => m.CreateTodoItem(It.IsAny<TodoItem>()))
            .Callback(() => { return; });
            mock.Setup(m => m.UpdateTodoItem(It.IsAny<TodoItem>()))
               .Callback(() => { return; });

            return mock;
        }
       
    }
}
