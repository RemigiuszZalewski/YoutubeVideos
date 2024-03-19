using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands.Movies.CreateMovie;
using Movies.Application.Commands.Movies.DeleteMovie;
using Movies.Application.Commands.Movies.UpdateMovie;
using Movies.Application.Queries.Movies.GetMovieById;
using Movies.Application.Queries.Movies.GetMovies;
using Movies.Contracts.Requests.Common;
using Movies.Contracts.Requests.Movies;
using Movies.Presentation.Extensions;

namespace Movies.Presentation.Modules;

public static class MoviesModule
{
    public static void AddMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/movies", async (IMediator mediator, [FromQuery] int pageSize, [FromQuery] int pageNumber, CancellationToken ct) =>
        {
            var paginatedMovieDtos = await mediator.Send(new GetMoviesQuery
                (new PaginationParams {PageSize = pageSize, PageNumber = pageNumber}), ct);
            return Results.Extensions.OkPaginationResult(paginatedMovieDtos.PageSize, paginatedMovieDtos.CurrentPage,
                paginatedMovieDtos.TotalCount, paginatedMovieDtos.TotalPages, paginatedMovieDtos.Items);
        }).WithTags("Movies");

        app.MapGet("/api/movies/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var movie = await mediator.Send(new GetMovieByIdQuery(id), ct);
            return Results.Ok(movie);
        }).WithTags("Movies");

        app.MapPost("/api/movies", async (IMediator mediator, CreateMovieRequest createMovieRequest,
            CancellationToken ct) =>
        {
            var command = new CreateMovieCommand(createMovieRequest.Title, createMovieRequest.Description,
                createMovieRequest.Category);
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        }).WithTags("Movies");

        app.MapPut("/api/movies/{id}", async (IMediator mediator, int id,
            UpdateMovieRequest updateMovieRequest, CancellationToken ct) =>
        {
            var command = new UpdateMovieCommand(id, updateMovieRequest.Title, updateMovieRequest.Description,
                updateMovieRequest.Category);
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        }).WithTags("Movies");

        app.MapDelete("/api/movies/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
        {
            var command = new DeleteMovieCommand(id);
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        }).WithTags("Movies");
    }
}