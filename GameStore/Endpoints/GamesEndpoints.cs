using System;
using GameStore.Data;
using GameStore.DTOs;
using GameStore.Entities;
using GameStore.Mapping;

namespace GameStore.Endpoints;

public static class GameEndpoint
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> _games = [
        // new(1, "Battle Arena X", "Fighting", 142_000, new DateOnly(2018,01,18)),
        // new(2, "Fist of Fury", "Fighting", 170_000, new DateOnly(2016, 08,16)),
        // new (3, "Dragon Questors", "Role-Playing", 423_528M, new DateOnly(2024, 3, 30)),
        // new (4, "Mystic Journey", "Role-Playing", 103_085M, new DateOnly(2022, 6, 24)),
        // new (5, "Soccer Stars 2025", "Sports", 255_300M, new DateOnly(2023, 9, 17)),
        // new (6, "Basketball Pro League", "Sports", 312_900M, new DateOnly(2020, 4, 2)),
        // new (7, "Speed Horizon", "Racing", 289_500M, new DateOnly(2016, 2, 20)),
        // new (8, "Turbo Drift", "Racing", 345_600M, new DateOnly(2018, 5, 15)),
        // new (9, "Funland Adventures", "Kids and Family", 150_000M, new DateOnly(2015, 10, 30)),
        // new (10, "Puzzle Party", "Kids and Family", 220_450M, new DateOnly(2019, 3, 19)),
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
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(game.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToDto());
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
