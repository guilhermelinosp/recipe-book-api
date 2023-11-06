using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionForgotPassword : ExceptionBase
{
    public ExceptionForgotPassword(string message) : base(message)
    {
    }

    protected ExceptionForgotPassword(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}