using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using taskBoardApi.Models;
using taskBoardApi.Services;

namespace taskBoardApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : Controller
{
   private readonly ITodoService _todoService;

   public TodosController(ITodoService todos)
   {
      _todoService = todos;
   }

   [HttpGet]
   public async Task<IEnumerable<Todo>> Get(int skip, int count)
   {
      return await _todoService.GetTodoAsync(skip, count);
   }

   [HttpPost]
   public async Task<ActionResult<Todo>> Create(Todo todo)
   {
      var newTodo = todo with { Id= ObjectId.GenerateNewId().ToString() };
      await _todoService.CreateTodoAsync(newTodo);

      return newTodo;
   }
}