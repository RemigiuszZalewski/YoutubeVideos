using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts.Exceptions;
using Movies.Contracts.Responses;
using Movies.Domain.Entities;
using Movies.Infrastructure;

namespace Movies.Application.Queries.Movies.GetMovieById;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, GetMovieByIdResponse>
{
    private readonly MoviesDbContext _moviesDbContext;

    public GetMovieByIdQueryHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    
    public async Task<GetMovieByIdResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _moviesDbContext.Movies
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (movie is null)
        {
            throw new NotFoundException($"{nameof(Movie)} with {nameof(Movie.Id)}: {request.Id}" +
                                        $"was not found in database");
        }

        return movie.Adapt<GetMovieByIdResponse>();
    }
}