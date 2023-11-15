using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountForgotPasswordException : BaseException
{
    public List<string>? ErrorMessages { get; set; }

    public AccountForgotPasswordException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }


    protected AccountForgotPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}