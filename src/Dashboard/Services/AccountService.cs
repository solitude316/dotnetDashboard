using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text; 
using Dashboard.Dto;
using Dashboard.Entities;
using Dashboard.Enums.Account;
using Dashboard.Exceptions;
using Dashboard.Helpers;
using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

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
            status = AccountStatus.Active,
            source = AccountSource.Registration
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

        var token = JWTHelper.GenerateToken(account.email, _configuration["JwtSettings:SignKey"]!, _configuration["JwtSettings:Issuer"]!);

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