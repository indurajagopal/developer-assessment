using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TodoList.Api.Model;
using TodoList.Api.Model.DTO;
using TodoList.Api.Repository.IRepository;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsRepository _dbTodoItems;
        public APIResponse _apiResponse;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemsRepository dbTodoItems, IMapper mapper)
        {
            _dbTodoItems = dbTodoItems;
            _mapper = mapper;
            this._apiResponse = new APIResponse();
        }

        // GET: api/TodoItems
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCompletedTodoItems()
        {
            try
            {
                var toDoLists = await _dbTodoItems.GetAllCompltedTodoItems();
                _apiResponse.Result = _mapper.Map<List<TodoItemDTO>>(toDoLists).Where(x => !x.IsCompleted).ToList(); 
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string>() { ex.ToString() };
                return _apiResponse;
            }
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}",Name ="GetToDoItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTodoItem(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                var result = await _dbTodoItems.GetTodoItem(u=>u.Id == id);

                if (result == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return _apiResponse;
                }
                _apiResponse.Result= _mapper.Map<TodoItemDTO>(result);
                _apiResponse.StatusCode=HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string>() { ex.ToString() };
                return _apiResponse;
            }
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateTodoItem(Guid id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id || todoItemDTO == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return _apiResponse;
            }
            if (await _dbTodoItems.GetAsync(u => u.Id == todoItemDTO.Id) == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                return _apiResponse;
            }
             try
            {
                TodoItem todoItem = _mapper.Map<TodoItem>(todoItemDTO);
                await _dbTodoItems.UpdateTodoItem(todoItem);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string>() { ex.ToString() };
                return _apiResponse;
            }

        }

        // POST: api/TodoItems 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null)
            {
                return BadRequest();
            }
            if (await _dbTodoItems.GetAsync(u => u.Id == todoItemDTO.Id) != null)
            {
                ModelState.AddModelError("customError", "Todo Item already exists!");
                return BadRequest();
            }
            if (string.IsNullOrEmpty(todoItemDTO?.Description))
            {
                return BadRequest("Description is required");
            }
            try
            {
                TodoItem todoItem = _mapper.Map<TodoItem>(todoItemDTO);
                await _dbTodoItems.CreateTodoItem(todoItem);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string>() { ex.ToString() };
                return _apiResponse;
            }
        }

    }
}
