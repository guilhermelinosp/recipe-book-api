using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionConflict : ExceptionBase
{
    public ExceptionConflict(string message) : base(message)
    {
    }

    protected ExceptionConflict(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}