namespace Movies.Contracts.Errors;

public class ValidationError
{
    public string Property { get; set; }
    public string ErrorMessage { get; set; }
}