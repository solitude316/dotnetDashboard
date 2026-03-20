using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;

namespace Dashboard.Services.JWT;

public class RS256Service : IJWTService
{
    private IConfiguration _configuration;

    private RSA _rsaPublic;

    private RSA _rsaPrivate;
    
    public RS256Service(IConfiguration configuration)
    {
        _configuration = configuration;

        var rsaPublicKeyPath = Environment.GetEnvironmentVariable("RSA_PUBLIC_KEY_PATH") 
                ?? throw new InvalidOperationException("Undefined environment variable RSA_PUBLIC_KEY_PATH.");
        var rsaPrivateKeyPath = Environment.GetEnvironmentVariable("RSA_PRIVATE_KEY_PATH")
                ?? throw new InvalidOperationException("Undefined environment variable RSA_PRIVATE_KEY_PATH.");

        _rsaPublic = RSA.Create();
        _rsaPublic.ImportFromPem(File.ReadAllText(rsaPublicKeyPath).ToCharArray());
        
        _rsaPrivate = RSA.Create();
        _rsaPrivate.ImportFromPem(File.ReadAllText(rsaPrivateKeyPath).ToCharArray());
    }

    public String Generate(Dictionary<string, object> param)
    {
        var tokenBuilder = JwtBuilder.Create()
                    .WithAlgorithm(new RS256Algorithm(_rsaPublic, _rsaPrivate));

        foreach(KeyValuePair<string, object> entry in param)
        {
            tokenBuilder.AddClaim(entry.Key, entry.Value);
        }
        
        return tokenBuilder.Encode();
    }

    public bool Validate(String token)
    {
        bool result = false;

        return result;
    }
}
