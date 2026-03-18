using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Dashboard.Dto;
using Dashboard.Entities;
using Dashboard.Enums.Account;
using Dashboard.Exceptions;
using Dashboard.Repositories;

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

        // TODO Role 列表須由 db 查詢。

        var claimList = new List<Claim>();
        claimList.Add(new Claim(ClaimTypes.Email, email));


        // var claims = new[]
        // {
        //     new Claim(ClaimTypes.Email, email)
        //     // new Claim(ClaimTypes.Role, "admin")
        // };

        var roles = await _accountRepository.getUserRoles(account.id);

        foreach(Role role in roles)
        {
            claimList.Add(new Claim(ClaimTypes.Role, role.title));
        }

        int claimSize = claimList.Count();

        Claim[] claims = new Claim[claimSize];

        for(int index = 0; index < claimSize; index++)
        {
            claims[index] = claimList[index];
        }

        // TOD audience 需要修改。
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SignKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
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