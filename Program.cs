using Microsoft.EntityFrameworkCore;
using gameleaderboardapi.Data;
using gameleaderboardapi.Repositories;
using gameleaderboardapi.Middlewares;
using gameleaderboardapi.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//if this line is not used, api wont read the file LeaderboardController.cs
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// to add many dbcotnext, change dbcontext class name
builder.Services.AddDbContext<ScoreDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<ILeaderboardRepo, LeaderboardRepo>();

//we can create a chain of exception handlers by adding multiple handlers here

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });

}

//change http to https
app.UseHttpsRedirection();

app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ApiKeyMiddleware>();
}

//if this line is not used, system will return "404 NOT FOUND" when trying to access api/leaderboard
app.MapControllers();

app.Run();
