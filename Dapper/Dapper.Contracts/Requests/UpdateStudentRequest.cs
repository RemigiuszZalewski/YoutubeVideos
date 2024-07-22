namespace Dapper.Contracts.Requests;

public record UpdateStudentRequest(int Id, string FirstName, string LastName, string EmailAddress, string Major);