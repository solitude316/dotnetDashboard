using System;
using System.Data;
using Dashboard.Dto;
using Dashboard.Entities;

namespace Dashboard.Repositories;

public interface IAccountRepository : IAbstractRepository
{
    Task<int> AddAsync(Account account);

    // Task<IEnumerable<Account>> SearchAsync(UserFilterDto filter);
    Task<Account?> SearchByEmailAsync(string email);

    Task<IEnumerable<Role>> getUserRoles(Guid id);
}