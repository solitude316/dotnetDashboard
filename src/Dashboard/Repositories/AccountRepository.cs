using System.Data;
using Dashboard.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;

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

    public async Task<IEnumerable<Account>> SearchAsync(string keyword)
    {
        throw new NotImplementedException();
    }
}