using System;
using GameStore.Data;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        // GET /genres
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Genres.Select(genre => genre.ToDto()).AsNoTracking().ToListAsync());

        // GET /genres/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Genre? genre = await dbContext.Genres.FindAsync(id);

            return genre is not null ? Results.Ok(genre.ToDto()) : Results.NotFound();
        });

        return group;
    }
}
