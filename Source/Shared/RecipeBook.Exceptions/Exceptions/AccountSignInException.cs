using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountSignInException : BaseException
{
    public List<string>? ErrorMessages { get; set; }

    public AccountSignInException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected AccountSignInException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
