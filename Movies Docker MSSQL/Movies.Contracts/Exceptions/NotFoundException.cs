namespace Movies.Contracts.Exceptions;

public class NotFoundException(string message) : Exception(message);