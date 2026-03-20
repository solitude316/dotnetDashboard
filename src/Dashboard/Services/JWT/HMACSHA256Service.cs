using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Dashboard.Services.JWT;

public class HMACSHA256Service : IJWTService
{
    IConfiguration _configuration;

    public HMACSHA256Service(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public String Generate(Dictionary<String, object> payload)
    {
        var claimList = new List<Claim>();

        foreach(KeyValuePair<string, object> entry in payload) 
        {
            claimList.Add(new Claim(entry.Key, entry.Value.ToString()));
        }

        int claimSize = claimList.Count();
        Claim[] claims = new Claim[claimSize];

        for(int index = 0; index < claimSize; index++)
        {
            claims[index] = claimList[index];
        }

        var issuer = _configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("Undefined Configuration variable JwtSettings:Issuer");
        var signKey = _configuration["JwtSettings:SignKey"] ?? throw new InvalidOperationException("Undefined Configuration variable JwtSettings:SignKey");
        // TOD audience 需要修改。
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool Validate (String token)
    {
        bool result = false;

        return result;
    }
}