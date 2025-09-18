using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOs;

public record class UpdateGameDto(
    [Required][StringLength(30)]string Name,
    [Required][StringLength(20)]string Genre,
    [Required][Range(50000, 500000)]decimal Price,
    DateOnly ReleaseDate
);
