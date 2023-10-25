using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionEmailSignUp : ExceptionBase
{
    public ExceptionEmailSignUp(string message) : base(message)
    {
    }

    protected ExceptionEmailSignUp(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}