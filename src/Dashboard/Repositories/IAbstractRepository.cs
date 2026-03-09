using System.Data;

namespace Dashboard.Repositories;

public interface IAbstractRepository
{
    IDbTransaction BeginTransaction();

    Task<IDbTransaction> BeginTransactionAsync();

    void CommitTransaction(IDbTransaction transaction);

    Task CommitTransactionAsync(IDbTransaction transaction);

    void RollbackTransaction(IDbTransaction transaction);

    Task RollbackTransactionAsync(IDbTransaction transaction);
}