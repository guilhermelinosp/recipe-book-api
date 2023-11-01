using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionSignIn : ExceptionBase
{
    public ExceptionSignIn(string message) : base(message)
    {
    }

    protected ExceptionSignIn(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
