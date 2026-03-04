using System.Data;
using Dashboard.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;

namespace Dashboard.Repositories;

public class UserRepository : IUserRepository
{
    IDbConnection _dbConnection;
    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var sql = "SELECT * FROM user";
        var result = await _dbConnection.QueryAsync<User>(sql);
        return result;
    }

    public async Task<User> Add(User user)
    {
        user.Id = Guid.NewGuid();
        var sql = $"INSERT INTO users (id, first_name, last_name, gender, birthday) VALUES (@Id, @FirstName, @LastName, @Gender, @Birthday)";
        var result = await _dbConnection.ExecuteAsync(sql, user);
        return user;
    }
}

