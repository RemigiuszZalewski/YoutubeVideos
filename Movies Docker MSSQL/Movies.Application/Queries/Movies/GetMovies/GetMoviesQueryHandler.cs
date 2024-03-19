using Mapster;
using MediatR;
using Movies.Application.Helpers;
using Movies.Contracts.Dtos;
using Movies.Contracts.Responses;
using Movies.Infrastructure;

namespace Movies.Application.Queries.Movies.GetMovies;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, PaginatedList<MovieDto>>
{
    private readonly MoviesDbContext _moviesDbContext;

    public GetMoviesQueryHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    
    public async Task<PaginatedList<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var getMoviesQuery = _moviesDbContext.Movies.ProjectToType<MovieDto>().AsQueryable();
        
        var paginatedList = await CollectionHelper<MovieDto>.ToPaginatedList(getMoviesQuery,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize);

        return paginatedList;
    }
}