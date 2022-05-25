using taskBoardApi.Models;

namespace taskBoardApi.Services;

public interface ITodoService
{
    Task<List<Todo>> GetTodoAsync(int skip, int count);
    Task<Todo> GetTodoById(string id);
    Task CreateTodoAsync(Todo todo);
    Task<Todo> UpdateTodoAsync(Todo todo);
    Task DeleteTodoAsync(string id);
}