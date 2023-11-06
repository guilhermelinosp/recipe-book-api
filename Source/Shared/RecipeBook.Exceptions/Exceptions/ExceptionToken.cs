using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionToken : ExceptionBase
{
    public ExceptionToken(string message) : base(message)
    {
    }

    protected ExceptionToken(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}