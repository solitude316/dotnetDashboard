using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text; 
using Microsoft.AspNetCore.Mvc;
using Dashboard.Dto;
using Dashboard.Entities;
using Dashboard.Enums.Account;
using Dashboard.Exceptions;
using Dashboard.Repositories;
using Dashboard.Services.JWT;

namespace Dashboard.Services;

public class AccountService : IAccountService
{
    private IAccountRepository _accountRepository;
    private IConfiguration _configuration;

    public AccountService(IConfiguration configuration, IAccountRepository accountRepository)
    {
        _configuration = configuration;
        _accountRepository = accountRepository;
    }

    public async Task<Account> RegisterAsync(AccountDto account)
    {
        if (account.Email == null)
        {
            throw new AccountException("email_is_required");
        }

        if(account.Password == null)
        {
            throw new AccountException("password_is_required");
        }

        var existingAccounts = await _accountRepository.SearchByEmailAsync(account.Email);
        if (existingAccounts != null)
        {
            throw new AccountException("account_exists");
        }

        var entity = new Account
        {
            id = Guid.NewGuid(),
            email = account.Email,
            password = HashPassword(account.Password),
            status = AccountStatusEnum.Active,
            source = AccountSourceEnum.Registration
        };

        using (var transaction = await _accountRepository.BeginTransactionAsync()) {
            try
            {
                var createdAccount = await _accountRepository.AddAsync(entity);
                await _accountRepository.CommitTransactionAsync(transaction);
                return entity;
            } catch (Exception ex) {
                await _accountRepository.RollbackTransactionAsync(transaction);
                var message = ex.Message;
                throw;
            }
        }
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        Account? account = await _accountRepository.SearchByEmailAsync(email);
        if(account == null)
        {
            throw new AccountException("user_not_found");
        }

        var hashedPassword = HashPassword(password);
        if (account.password != hashedPassword) {
            throw new AccountException("invalid_credentials");
        }

        var SignKey = _configuration["JwtSettings:SignKey"]!;
        var issuer = _configuration["JwtSettings:Issuer"]!;

        if(SignKey == null || issuer == null)
        {
            throw new Exception("undefined_jwt_singkey_or_issuer");
        }

        var roles = await _accountRepository.getUserRolesAsync(account.id);

        // V1 使用 HMACSHA256;
        IJWTService jwtService = new HMACSHA256Service(_configuration);

        // V2 使用 JWT + RSA 加密
        // IJWTService jwtService = new RS256Service(_configuration);
        var tokenParams = new Dictionary<String, object>();

        tokenParams.Add("issuer", issuer);
        tokenParams.Add(ClaimTypes.Email, account.email);
        tokenParams.Add("Audience", account.email);
        tokenParams.Add("exp", DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds());

        foreach(Role role in roles)
        {
            tokenParams.Add(ClaimTypes.Role, role.code);
        }

        String token = jwtService.Generate(tokenParams);
        
        return token;
    }

    private string HashPassword(string password)
    {
        using (var sha512 = SHA512.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha512.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}