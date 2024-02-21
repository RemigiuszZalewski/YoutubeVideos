using FluentValidation;
using Movies.Domain.Entities;

namespace Movies.Application.Commands.Movies.DeleteMovie;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .WithMessage($"{nameof(Movie.Id)} cannot be empty");
    }
}