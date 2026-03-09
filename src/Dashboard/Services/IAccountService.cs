using Dashboard.Entities;
using Dashboard.Dto;

namespace Dashboard.Services;

public interface IAccountService
{
    Task<Account> RegisterAsync(RegistDto account);
}