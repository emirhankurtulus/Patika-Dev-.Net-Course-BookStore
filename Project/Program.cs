using Microsoft.EntityFrameworkCore;
using Project.DBOperations;
using Project.Middlewares;
using Project.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "projectdb"));
builder.Services.AddScoped<IDbContext>(provider => provider.GetService<BookStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

Console.WriteLine("\r\n  _                     _             _               \r\n | |                   | |           | |              \r\n | | __  _   _   _ __  | |_   _   _  | |  _   _   ___ \r\n | |/ / | | | | | '__| | __| | | | | | | | | | | / __|\r\n |   <  | |_| | | |    | |_  | |_| | | | | |_| | \\__ \\\r\n |_|\\_\\  \\__,_| |_|     \\__|  \\__,_| |_|  \\__,_| |___/\r\n                                                      \r\n                                                      \r\n");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    DataGenerator.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
