using Dashboard.Entities;
using System.Data;

namespace Dashboard.Repositories;

public interface IAccountRepository : IAbstractRepository
{
    Task<int> AddAsync(Account account);

    Task<IEnumerable<Account>> SearchAsync(string keyword);
}