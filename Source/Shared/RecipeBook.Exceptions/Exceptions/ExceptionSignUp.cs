using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionSignUp : ExceptionBase
{
    public ExceptionSignUp(string message) : base(message)
    {
    }

    protected ExceptionSignUp(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}