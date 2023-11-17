using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountResetPasswordException : BaseException
{
    public AccountResetPasswordException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected AccountResetPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}