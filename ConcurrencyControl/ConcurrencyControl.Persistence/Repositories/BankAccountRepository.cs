using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace ConcurrencyControl.Persistence.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly DemoDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy;

    public BankAccountRepository(DemoDbContext context)
    {
        _context = context;
        _retryPolicy = Policy
            .Handle<DbUpdateConcurrencyException>()
            .WaitAndRetryAsync(3, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
    
    public async Task CreateBankAccountAsync(string accountNumber, string ownerName)
    {
        await _context.BankAccounts.AddAsync(new BankAccount
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName
        });

        await _context.SaveChangesAsync();
    }

    public async Task UpdateBalanceAsync(Guid accountId, decimal amount)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found");

            account.Balance += amount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await ex.Entries.Single().ReloadAsync();
                Console.WriteLine($"Concurrency exception occurred: {ex.Message}");
                throw;
            }
        });
    }
}