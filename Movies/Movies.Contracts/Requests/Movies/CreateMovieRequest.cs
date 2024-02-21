namespace Movies.Contracts.Requests.Movies;

public record CreateMovieRequest(string Title, string Description, string Category);