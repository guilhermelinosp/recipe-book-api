using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class PhoneSignUpException : BaseException
{
    public PhoneSignUpException(string message) : base(message)
    {
    }

    protected PhoneSignUpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}