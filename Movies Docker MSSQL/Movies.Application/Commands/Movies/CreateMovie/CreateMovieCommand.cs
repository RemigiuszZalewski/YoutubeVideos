using MediatR;

namespace Movies.Application.Commands.Movies.CreateMovie;

public record CreateMovieCommand(string Title, string Description, string Category) : IRequest<int>;