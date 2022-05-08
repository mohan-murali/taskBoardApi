using taskBoardApi.Models;

namespace taskBoardApi.Services;

public interface ITodoService
{
    Task<List<Todo>> GetTodoAsync(int skip, int count);
    Task CreateTodoAsync(Todo todo);
    Task<Todo> UpdateTodoAsync(Todo todo);
}