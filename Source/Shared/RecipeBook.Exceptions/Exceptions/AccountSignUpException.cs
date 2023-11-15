using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class AccountSignUpException : BaseException
{
    public List<string>? ErrorMessages { get; set; }

    public AccountSignUpException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected AccountSignUpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}