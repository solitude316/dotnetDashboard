using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


using Otter.Core.Interfaces.Services;
using Otter.Core.Entities;

namespace Otter.Core.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    private readonly string _secretKey;

    private readonly string _issuer;

    private readonly string _audience;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _secretKey = _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey not found in configuration.");
        _issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer not found in configuration.");
        _audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience not found in configuration.");
    }

    public string GenerateAccessToken(UserEntity user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            // new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30), // Set token expiration time
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        // Implementation for generating a refresh token
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        // 這裡應該檢查資料庫中的 refresh token
        // 簡化版本，實際應用中需要儲存在資料庫
        return !string.IsNullOrEmpty(refreshToken);
    }

    public UserEntity GetUserFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            
            return new UserEntity
            {
                Id = Guid.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                Email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                // Role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value
            };
        }
        catch
        {
            return null;
        }
    }

    public DateTime GetTokenExpiration(string token)
    {
        // Implementation for getting token expiration
        throw new NotImplementedException();
    }
}