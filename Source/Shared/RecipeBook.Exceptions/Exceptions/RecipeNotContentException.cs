using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

public class RecipeNotContentException : BaseException
{
    public RecipeNotContentException(string message) : base(message)
    {
    }

    protected RecipeNotContentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}