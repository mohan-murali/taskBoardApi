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

    [HttpGet(template:"{id}", Name="GetTodoById")]
    public async Task<ActionResult<Todo>> GetTodoById(string id)
    {
        var todo = await _todoService.GetTodoById(id);
    
        if (todo == null)
        {
            return NotFound();
        }
    
        return todo;
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create(Todo todo)
    {
        var newTodo = todo with { Id = ObjectId.GenerateNewId().ToString() };
        await _todoService.CreateTodoAsync(newTodo);
    
        return newTodo;
    }
    
    [HttpPut]
    public async Task<ActionResult<Todo>> Update(Todo update)
    {
        var todo = await _todoService.UpdateTodoAsync(update);
    
        return CreatedAtRoute("GetTodoById", new { id = todo.Id }, todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _todoService.DeleteTodoAsync(id);

        return Ok();
    }
}