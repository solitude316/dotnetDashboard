using Dashboard.Entities;
using Dashboard.Dto;

namespace Dashboard.Services;

public interface IAccountService
{
    Task<Account> RegisterAsync(AccountDto account);

    Task<string> LoginAsync(string email, string password);
}