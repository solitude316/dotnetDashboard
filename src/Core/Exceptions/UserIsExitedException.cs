
namespace Otter.Core.Exceptions;

public class UserIsExitedException : Exception
{
    public UserIsExitedException()
        : base("User already exists.")
    {
    }

    public UserIsExitedException(string message)
        : base(message)
    {
    }

    public UserIsExitedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}