using System;
using System.ComponentModel.DataAnnotations;
namespace TodoList.Api.Model.DTO
{
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
