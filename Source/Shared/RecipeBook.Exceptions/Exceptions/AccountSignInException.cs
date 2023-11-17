using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountSignInException : BaseException
{
    public AccountSignInException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected AccountSignInException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}