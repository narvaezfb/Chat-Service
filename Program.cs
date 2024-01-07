using Chat_Service.Data;
using Chat_Service.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddDbContext<ChatServiceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000") 
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials(); 
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("chat-hub");

app.MapControllers();

app.Run();

