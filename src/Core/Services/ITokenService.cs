using Otter.Core.Entities;

namespace Otter.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(UserEntity user);

    string GenerateRefreshToken();

    bool ValidateRefreshToken(string refreshToken);

    UserEntity GetUserFromToken(string token);

    DateTime GetTokenExpiration(string token);
}