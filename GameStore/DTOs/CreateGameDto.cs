using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOs;

public record class CreateGameDto(
    [Required][StringLength(30)] string Name,
    int GenreId,
    [Required][Range(50000, 500000)]decimal Price,
    DateOnly ReleaseDate
);
