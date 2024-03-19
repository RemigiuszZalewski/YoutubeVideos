using Mapster;
using Movies.Contracts.Dtos;
using Movies.Contracts.Responses;
using Movies.Domain.Entities;

namespace Movies.Application.Mappings;

public class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<PaginatedList<MovieDto>, GetMoviesResponse>.NewConfig()
            .Map(dest => dest.Results, src => src);
        
        TypeAdapterConfig<Movie, GetMovieByIdResponse>.NewConfig()
            .Map(dest => dest.MovieDto, src => src);
    }
}