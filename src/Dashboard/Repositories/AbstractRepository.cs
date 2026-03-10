using System.Data;
using Dapper;

namespace Dashboard.Repositories;

public class AbstractRepository : IDisposable
{
    protected readonly IDbConnection _dbConnection;

    public AbstractRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }

    public void Dispose()
    {
        if (_dbConnection != null && _dbConnection.State != ConnectionState.Closed)
        {
            _dbConnection.Close();
        }
    }

    public IDbTransaction BeginTransaction()
    {
        return _dbConnection.BeginTransaction();
    }

    public Task<IDbTransaction> BeginTransactionAsync()
    {
        return Task.FromResult(_dbConnection.BeginTransaction());
    }

    public void CommitTransaction(IDbTransaction transaction)
    {
        transaction.Commit();
    }

    public Task CommitTransactionAsync(IDbTransaction transaction)
    {
        transaction.Commit();
        return Task.CompletedTask;
    }

    public void RollbackTransaction(IDbTransaction transaction)
    {
        transaction.Rollback();
    }

    public Task RollbackTransactionAsync(IDbTransaction transaction)
    {
        transaction.Rollback();
        return Task.CompletedTask;
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(Dictionary<string, object> filters, Dictionary<string, string> order)
    {
        SqlBuilder builder = new SqlBuilder();
        string query = $"SELECT * FROM {typeof(T).GetType().GetProperty("Table")} /**where**/";
        SqlBuilder.Template template = builder.AddTemplate(query);

        DynamicParameters dynamicParams = new DynamicParameters();
        foreach (var parameter in filters)
        {
            builder.Where($"{parameter.Key} = @{parameter.Key}");
            dynamicParams.Add($"@{parameter.Key}", parameter.Value);
        }
        string rawSql = template.RawSql;
        return _dbConnection.Query<T>(rawSql, dynamicParams).AsList();
    }
}