using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionResetPassword : ExceptionBase
{
    public ExceptionResetPassword(string message) : base(message)
    {
    }

    protected ExceptionResetPassword(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}