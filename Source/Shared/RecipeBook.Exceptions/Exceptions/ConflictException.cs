using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ConflictException : BaseException
{
    public ConflictException(string message) : base(message)
    {
    }

    protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}