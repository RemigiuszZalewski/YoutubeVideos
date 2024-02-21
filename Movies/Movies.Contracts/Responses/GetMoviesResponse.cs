using Movies.Contracts.Dtos;

namespace Movies.Contracts.Responses;

public record GetMoviesResponse(PaginatedList<MovieDto> Results);