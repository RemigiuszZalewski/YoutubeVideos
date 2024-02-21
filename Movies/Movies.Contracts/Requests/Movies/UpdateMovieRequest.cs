namespace Movies.Contracts.Requests.Movies;

public record UpdateMovieRequest(string Title, string Description, string Category);