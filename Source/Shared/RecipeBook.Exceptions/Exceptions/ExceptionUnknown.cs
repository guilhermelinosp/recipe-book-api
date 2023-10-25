using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionUnknown : ExceptionBase
{
    public ExceptionUnknown(string message) : base(message)
    {
    }

    protected ExceptionUnknown(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}