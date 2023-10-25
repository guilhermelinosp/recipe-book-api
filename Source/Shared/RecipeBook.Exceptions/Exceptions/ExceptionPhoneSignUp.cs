using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionPhoneSignUp : ExceptionBase
{
    public ExceptionPhoneSignUp(string message) : base(message)
    {
    }

    protected ExceptionPhoneSignUp(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}