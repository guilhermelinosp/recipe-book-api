using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionBase : SystemException
{
    public ExceptionBase(string message) : base(message)
    {
    }

    protected ExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
