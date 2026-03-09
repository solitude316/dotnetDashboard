using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Dashboard.Entities;
using Dashboard.Repositories;
using Dashboard.Dto;
using Dashboard.Enums.Account;

namespace Dashboard.Services;

public class AccountService : IAccountService
{
    private IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> RegisterAsync(RegistDto account)
    {
        var entity = new Account
        {
            id = Guid.NewGuid(),
            email = account.email,
            password = HashPassword(account.password),
            status = AccountStatus.Active,
            source = AccountSource.Registration
        };

        using (var transaction = await _accountRepository.BeginTransactionAsync()) {
            try
            {
                var createdAccount = await _accountRepository.AddAsync(entity);

                await _accountRepository.CommitTransactionAsync(transaction);

                return entity;
            } catch (Exception ex) {
                await _accountRepository.RollbackTransactionAsync(transaction);
                var message = ex.Message;
                throw;
            }
        }
    }

    private string HashPassword(string password)
    {
        return password; // Implement password hashing logic here.
    }
}