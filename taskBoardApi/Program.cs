using MongoDB.Driver;
using taskBoardApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

//Add mongoDB connection settings
var connectionString = builder.Configuration["DatabaseSettings:MongoConnectionString"];
var databaseName = builder.Configuration["DatabaseSettings:DatabaseName"];

//register DB
builder.Services.AddScoped(c => new MongoClient(connectionString).GetDatabase(databaseName));

//register services
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
