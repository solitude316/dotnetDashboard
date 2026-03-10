using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Dashboard.Dto;
using Dashboard.Entities;

namespace Dashboard.Repositories;

public class AccountRepository : AbstractRepository, IAccountRepository
{

    public AccountRepository(IDbConnection dbConnection) : base(dbConnection)
    {
        
    }

    public async Task<int> AddAsync(Account account)
    {
        var sql = "INSERT INTO accounts (id, source, email, password, status) VALUES (@id, @source, @Email, @password, @status)";
        var result = await this._dbConnection.ExecuteAsync(sql, account);
        return result;
    }

    public async Task<Account?> SearchByEmailAsync(string email)
    {
        var sql = "SELECT * FROM accounts WHERE email = @Email";
        var result = await this._dbConnection.QueryFirstOrDefaultAsync<Account?>(sql, new { Email = email });
        return result;
    }

    // public async Task<IEnumerable<Account>> SearchAsync(UserFilterDto filter)
    // {
    //     Dictionary<string, object> filters = new Dictionary<string, object>();
    //     filters["email"] = filter.Email!;
    //     return await this.QueryAsync<Account>(filters, new Dictionary<string, string>());
    // }
}