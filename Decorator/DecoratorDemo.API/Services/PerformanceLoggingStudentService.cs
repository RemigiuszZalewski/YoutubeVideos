using System.Diagnostics;
using DecoratorDemo.API.Abstracts;
using DecoratorDemo.API.Models;

namespace DecoratorDemo.API.Services;

public class PerformanceLoggingStudentService : IStudentService
{
    private readonly IStudentService _studentService;
    private readonly ILogger<PerformanceLoggingStudentService> _logger;
    private readonly Stopwatch _stopwatch;

    public PerformanceLoggingStudentService(IStudentService studentService,
        ILogger<PerformanceLoggingStudentService> logger)
    {
        _studentService = studentService;
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    
    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        _stopwatch.Restart();
        var result = await _studentService.GetStudentsAsync();
        
        _stopwatch.Stop();
        _logger.LogInformation("Elapsed ms: {ElapsedMs}", _stopwatch.ElapsedMilliseconds);

        return result;
    }
}