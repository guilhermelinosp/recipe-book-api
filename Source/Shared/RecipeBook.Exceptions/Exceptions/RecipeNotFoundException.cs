using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

public class RecipeNotFoundException : BaseException
{
    public RecipeNotFoundException(string message) : base(message)
    {
    }

    protected RecipeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}