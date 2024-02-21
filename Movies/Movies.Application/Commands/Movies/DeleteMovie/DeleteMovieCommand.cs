using MediatR;

namespace Movies.Application.Commands.Movies.DeleteMovie;

public record DeleteMovieCommand(int Id) : IRequest<Unit>;