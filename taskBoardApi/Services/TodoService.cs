using MongoDB.Driver;
using taskBoardApi.Models;

namespace taskBoardApi.Services;

public class TodoService : ITodoService
{
    private readonly IMongoCollection<Todo> _todo;

    public TodoService(IMongoDatabase db)
    {
        _todo = db.GetCollection<Todo>(typeof(Todo).Name);
    }

    public async Task<List<Todo>> GetTodoAsync(int skip, int count) =>
        await _todo.Find(todo => true).Skip(skip).Limit(count).ToListAsync();

    public Task<Todo> GetTodoById(string id) => _todo.Find(t => t.Id == id).FirstOrDefaultAsync();

    public Task CreateTodoAsync(Todo todo) => _todo.InsertOneAsync(todo);

    public Task<Todo> UpdateTodoAsync(Todo todo) => _todo.FindOneAndReplaceAsync(
        Builders<Todo>.Filter.Eq(t => t.Id, todo.Id), todo,
        new FindOneAndReplaceOptions<Todo>
            { ReturnDocument = ReturnDocument.After });

    public Task DeleteTodoAsync(string id) => _todo.DeleteOneAsync(t => t.Id == id);
}