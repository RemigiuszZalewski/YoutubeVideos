using MediatR;
using Movies.Domain.Entities;
using Movies.Infrastructure;

namespace Movies.Application.Commands.Movies.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly MoviesDbContext _moviesDbContext;

    public CreateMovieCommandHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    
    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Category = request.Category,
            Description = request.Description,
            CreateDate = DateTime.Now.ToUniversalTime()
        };

        await _moviesDbContext.Movies.AddAsync(movie, cancellationToken);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);

        return movie.Id;
    }
}