using Dapper.Contracts.Requests;
using Dapper.Domain.Abstracts.Persistence;
using Dapper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.API.Modules;

public static class StudentModule
{
    public static void AddStudentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/students", async (IGenericRepository<Student> studentRepository) =>
        {
            var students = await studentRepository.GetAllAsync();
            return Results.Ok(students);
        });

        app.MapGet("/api/students/{id}", async (int id, IGenericRepository<Student> studentRepository) =>
        {
            Student? student = await studentRepository.GetByIdAsync(id);

            return student == null ? Results.NotFound() : Results.Ok(student);
        });
        
        app.MapDelete("/api/students/{id}", async (int id, IGenericRepository<Student> studentRepository) =>
        {
            bool result = await studentRepository.DeleteAsync(id);

            return result ? Results.NoContent() : Results.NotFound();
        });
        
        app.MapPost("/api/student", async ([FromBody] AddStudentRequest addStudentRequest,
            IGenericRepository<Student> studentRepository) =>
        {
            var student = new Student
            {
                FirstName = addStudentRequest.FirstName,
                LastName = addStudentRequest.LastName,
                EmailAddress = addStudentRequest.EmailAddress,
                Major = addStudentRequest.Major
            };
            
            bool result = await studentRepository.AddAsync(student);

            return result ? Results.Created() : Results.Problem();
        });

        app.MapPut("/api/students", async ([FromBody] UpdateStudentRequest updateStudentRequest,
            IGenericRepository<Student> studentRepository) =>
        {
            var student = new Student
            {
                Id = updateStudentRequest.Id,
                FirstName = updateStudentRequest.FirstName,
                LastName = updateStudentRequest.LastName,
                EmailAddress = updateStudentRequest.EmailAddress,
                Major = updateStudentRequest.Major
            };
            
            bool result = await studentRepository.UpdateAsync(student);

            return result ? Results.Ok(student) : Results.Problem();
        });
    }
}