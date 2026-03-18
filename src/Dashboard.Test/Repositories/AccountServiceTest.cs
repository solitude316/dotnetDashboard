// using Xunit;
// using System;
// using System.Security.Cryptography;
// using System.Text;
// using Moq;
// using Dashboard.Repositories;
// using Dashboard.Entities;
// using Dashboard.Enums;
// using Dashboard.Services;

namespace Dashboard.Test.Repositories;

public class AccountServiceTest
{
    [Fact]
    public void test1()
    {

        // var sha512 = SHA512.Create();
        // var bytes = Encoding.UTF8.GetBytes("sin316");
        // var hash = sha512.ComputeHash(bytes);

        // var mockRepo = new Mock<IAccountRepository>();
        // mockRepo.Setup(repo => repo.SearchByEmailAsync("simple@gmail.com"));
        // mockRepo.Setup( r => r.SearchByEmailAsync("sample@gmail.com"))
        //     .Returns(new Account {
        //         id = Guid.NewGuid(),
        //         email = "sample@gmail.com",
        //         password = Convert.ToBase64String(hash),
        //         status = Dashboard.Enums.Account.AccountStatus.Active,
        //         last_login = DateTime.Now,
        //         source = Dashboard.Enums.Account.AccountSourceEnum.Registration,
        //         created_at = DateTime.Now,
        //         updated_at = DateTime.Now
        //     });

        // var accountService = new AccountService(mockRepo.Object);
        // var result = await accountService.LoginAsync("sample@gmail.com", "sin316");

    }

}