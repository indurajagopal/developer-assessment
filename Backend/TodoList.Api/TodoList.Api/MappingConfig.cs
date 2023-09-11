using AutoMapper;
using TodoList.Api.Model;
using TodoList.Api.Model.DTO;

namespace TodoList.Api
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}
