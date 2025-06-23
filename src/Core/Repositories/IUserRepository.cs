using Otter.Core.Entities;

namespace Otter.Core.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByCredentialsAsync(string email, string password);
    Task<UserEntity> CreateAsync(UserEntity user);
    Task UpdateAsync(UserEntity user);
    Task DeleteAsync(Guid id);
}