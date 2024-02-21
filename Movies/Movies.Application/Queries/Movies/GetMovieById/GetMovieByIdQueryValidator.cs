using FluentValidation;
using Movies.Domain.Entities;

namespace Movies.Application.Queries.Movies.GetMovieById;

public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .WithMessage($"{nameof(Movie.Id)} cannot be empty");
    }
}