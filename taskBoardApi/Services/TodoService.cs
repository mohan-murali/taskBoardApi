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

    public async Task<List<Todo>> GetTodoAsync(int skip, int count)
    {
        var result = await _todo.Find(todo => true).Skip(skip).Limit(count).ToListAsync();

        return result;
    }

    public Task CreateTodoAsync(Todo todo)
    {
        return _todo.InsertOneAsync(todo);
    }

    public Task<Todo> UpdateTodoAsync(Todo todo)
    {
        return _todo.FindOneAndReplaceAsync(Builders<Todo>.Filter.Eq(t => t.Id, todo.Id), todo,
            new FindOneAndReplaceOptions<Todo>
                { ReturnDocument = ReturnDocument.After });
    }
}