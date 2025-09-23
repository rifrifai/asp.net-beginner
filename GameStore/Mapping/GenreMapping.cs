using System;
using GameStore.DTOs;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class GenreMapping
{
	public static GenreDto ToDto(this Genre genre) => new(genre.Id, genre.Name);
}
