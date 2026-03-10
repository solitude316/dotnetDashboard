using Dashboard.Entities;
using System.Data;
using Dashboard.Dto;

namespace Dashboard.Repositories;

public interface IAccountRepository : IAbstractRepository
{
    Task<int> AddAsync(Account account);

    // Task<IEnumerable<Account>> SearchAsync(UserFilterDto filter);
    Task<Account> SearchByEmailAsync(string email);
}