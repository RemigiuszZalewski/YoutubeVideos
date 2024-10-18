namespace ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;

public interface IBankAccountRepository
{
    Task CreateBankAccountAsync(string accountNumber, string ownerName);
    Task UpdateBalanceAsync(Guid accountId, decimal amount);
}