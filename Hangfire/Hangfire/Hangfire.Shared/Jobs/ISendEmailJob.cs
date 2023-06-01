namespace Hangfire.Shared.Jobs;

public interface ISendEmailJob
{
    Task Execute();
}