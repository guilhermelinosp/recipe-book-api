using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountSignUpException : BaseException
{
    public AccountSignUpException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected AccountSignUpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}