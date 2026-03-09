using System.Data;
using Dashboard.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;

namespace Dashboard.Repositories;

public class UserRepository : AbstractRepository, IUserRepository
{
    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {
        
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var sql = "SELECT * FROM user";
        var result = await this._dbConnection.QueryAsync<User>(sql);
        return result;
    }

    public async Task<int> AddAsync(User user)
    {
        user.id = Guid.NewGuid();
        var sql = "INSERT INTO users (id, first_name, last_name, gender, birthday) VALUES (@Id, @FirstName, @LastName, @Gender, @Birthday)";
        var result = await this._dbConnection.ExecuteAsync(sql, user);
        return result;
    }

    public async Task<User?> GetById(Guid id)
    {
        var sql = "SELECT * FROM users WHERE id = @Id";
        var result = await this._dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { id = id });
        return result;
    }
}
