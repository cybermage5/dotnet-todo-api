using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dotnet_todo_api.src.TodoApi.Models;
using System.Linq;

namespace dotnet_todo_api.src.TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static readonly List<Todo> Todos =
        [
            new Todo { Id = 1, Name = "Learn ASP.NET Core", IsComplete = false },
            new Todo { Id = 2, Name = "Build a Web API", IsComplete = false }
        ];

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAllTodos()
        {
            return Todos;
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodoById(long id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost]
        public ActionResult<Todo> CreateTodo(Todo todo)
        {
            todo.Id = Todos.Max(t => t.Id) + 1;
            Todos.Add(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(long id, Todo updatedTodo)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.Name = updatedTodo.Name;
            todo.IsComplete = updatedTodo.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(long id)
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            Todos.Remove(todo);
            return NoContent();
        }
    }
}