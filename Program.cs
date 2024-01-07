using Chat_Service;
using Chat_Service.Data;
using Chat_Service.Hubs;
using Chat_Service.Middlewares;
using Chat_Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(c =>
{
    // Add JWT Bearer authentication information for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Include the JWT token in the Swagger UI
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddSignalR();

//Register Custom services
builder.Services.AddHttpClient<ITokenValidationService, TokenValidationService>();


var app = builder.Build();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/Messages"), appBuilder =>
{
    appBuilder.UseMiddleware<ValidateTokenMiddleware>();
});

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

