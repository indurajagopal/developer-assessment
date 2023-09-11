using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Api.Controllers;
using TodoList.Api.Model;
using TodoList.Api.Model.DTO;
using TodoList.Api.Repository.IRepository;
using TodoList.Api.UnitTests.Mocks;
using Xunit;

namespace TodoList.Api.UnitTests.Controller
{
    public class TodoItemsControllerTests
    {
        private readonly Mock<ITodoItemsRepository> _repositoryMock;
        public TodoItemsControllerTests()
        {
            _repositoryMock = new Mock<ITodoItemsRepository>();
        }
        public IMapper GetMapper()
        {
            var mappingConfig = new MappingConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingConfig));
            return new Mapper(configuration);
        }

        [Fact]
        public async void CreateTodoItem_WhenCreated_ThenCreatedReturns()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoController = new TodoItemsController(repositoryMock.Object,mapper);

            var toDoItem = new TodoItemDTO()
            {
                Id = new Guid("243b7f31-3ecc-4204-9594-230ed0a90d8c"),
                Description = "TestName",
                IsCompleted = false
            };
            ActionResult<APIResponse> result = await toDoController.CreateTodoItem(toDoItem);

            Assert.NotNull(result);
            var status = ((ObjectResult)result.Result).StatusCode;
            Assert.Equal((int)HttpStatusCode.OK, status);
        }
        [Fact]
        public async void UpdateTodoItem_WhenUpdated_ThenNotFoundReturns()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoController = new TodoItemsController(repositoryMock.Object,mapper);

            var id = Guid.Parse("243b7f31-3ecc-4204-9594-230ed0a90d8c");
            var toDoItem = new TodoItemDTO()
            {
                Id = new Guid("243b7f31-3ecc-4204-9594-230ed0a90d8c"),
                Description = "NewName",
                IsCompleted = false
            };
            ActionResult<APIResponse> result = await toDoController.UpdateTodoItem(id,toDoItem);

            Assert.Equal(HttpStatusCode.NotFound, result.Value.StatusCode);
        }
        [Fact]
        public async void UpdateTodoItem_WhenUpdated_ThenNoContentReturns()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoController = new TodoItemsController(repositoryMock.Object, mapper);

            var id = Guid.Parse("d409e407-cf63-4468-8189-75b0956ac13a");
            var toDoItem = new TodoItemDTO()
            {
                Id = new Guid("d409e407-cf63-4468-8189-75b0956ac13a"),
                Description = "NewName",
                IsCompleted = false
            };
            ActionResult<APIResponse> result = await toDoController.UpdateTodoItem(id, toDoItem);

            Assert.Equal(HttpStatusCode.NoContent, result.Value.StatusCode);
        }
        [Fact]
        public async void UpdateTodoItem_WhenUpdated_ThenBadRequestReturns()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoController = new TodoItemsController(repositoryMock.Object, mapper);

            var id = Guid.Parse("243b7f31-3ecc-4204-9594-230ed0a90d8c");
            var toDoItem = new TodoItemDTO();
            
            ActionResult<APIResponse> result = await toDoController.UpdateTodoItem(id, toDoItem);

            Assert.Equal(HttpStatusCode.BadRequest, result.Value.StatusCode);
        }
        [Fact]
        public async Task GetCompletedTodoItems_ShouldReturn200Status()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoItemsController = new TodoItemsController(repositoryMock.Object, mapper);

            var result = await toDoItemsController.GetCompletedTodoItems();

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result.Result).StatusCode);
            Assert.NotEmpty(((APIResponse)(((ObjectResult)result.Result).Value)).Result as List<TodoItemDTO>);
        }

        [Fact]
        public async Task GetTodoItem_ShouldReturnNotFoundStatus()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoItemsController = new TodoItemsController(repositoryMock.Object, mapper);
            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");

            var result = await toDoItemsController.GetTodoItem(id);

            Assert.Equal(HttpStatusCode.NotFound, result.Value.StatusCode);
        }

        [Fact]
        public async Task GetTodoItem_ShouldReturnResult()
        {
            var repositoryMock = MockTodoItemsRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var toDoItemsController = new TodoItemsController(repositoryMock.Object, mapper);
            var id = new Guid("29b0c44d-66cd-4801-ac59-b0007c1f883e");

            var result = await toDoItemsController.GetTodoItem(id);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Value.StatusCode);
        }

    }
}
