using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class EmailSignUpException : BaseException
{
    public EmailSignUpException(string message) : base(message)
    {
    }

    protected EmailSignUpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}