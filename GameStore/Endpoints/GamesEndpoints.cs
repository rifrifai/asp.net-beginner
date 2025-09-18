using System;
using GameStore.DTOs;

namespace GameStore.Endpoints;

public static class GameEndpoint
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> _games = [
        new (1, "The Silent Code", "Strategy", 150_000M, new DateOnly (2017, 3, 6)),
        new (2, "Echoes of Tomorrow", "Action", 325_000M, new DateOnly(2019, 6, 25)),
        new (3, "Crimson Horizon", "Adventure", 180_000M, new DateOnly(2021, 2, 27)),
        new (4, "Digital Dreams", "Simulation", 250_000M, new DateOnly(2023, 1, 15)),
        new (5, "Shadows of Reality", "Horror", 199_000M, new DateOnly(2016, 3, 15)),
        new (6, "Neon Pulse", "Shooter", 275_000M, new DateOnly(2018, 9, 20)),
        new (7, "Forgotten Paths", "RPG", 420_000M, new DateOnly(2020, 12, 8)),
        new (8, "Virtual Dawn", "Puzzle", 135_000M, new DateOnly(2022, 7, 30)),
        new (9, "Quantum Echo", "Adventure", 299_000M, new DateOnly(2015, 11, 3)),
        new (10, "Mystic Framework", "Simulation", 480_000M, new DateOnly(2024, 5, 19))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", () => _games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = _games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                _games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            _games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // UPDATE games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = _games.FindIndex(game => game.Id == id);

            if (index == -1) return Results.NotFound();

            _games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            var game = _games.RemoveAll(game => game.Id == id);

            if (game == 0) return Results.NotFound();

            return Results.NoContent();
        });

        return group;
    }
}
