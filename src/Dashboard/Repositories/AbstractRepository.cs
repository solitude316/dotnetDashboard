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
}