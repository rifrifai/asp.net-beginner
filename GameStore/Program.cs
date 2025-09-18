using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<GameStoreContext>("Data Source=GameStore.db");

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
