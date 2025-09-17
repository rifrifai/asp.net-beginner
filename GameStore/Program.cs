using GameStore.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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

// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// UPDATE games/{id}

// PATCH games/{id}

// DELETE games/{id}

app.Run();
