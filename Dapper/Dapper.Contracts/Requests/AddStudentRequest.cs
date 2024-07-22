namespace Dapper.Contracts.Requests;

public record AddStudentRequest(string FirstName, string LastName, string EmailAddress, string Major);