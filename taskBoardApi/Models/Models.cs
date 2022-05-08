using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace taskBoardApi.Models;

public record Todo(string Description, User User, Status Status)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }
}

public record User(string Id, string Name);

public enum Status
{
    Ready,
    InProgress,
    Completed
}
    

