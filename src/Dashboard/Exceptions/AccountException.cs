using System;

namespace Dashboard.Exceptions;

public class AccountException : Exception
{
    public AccountException(string message)
        : base(message)
    {
    }
}