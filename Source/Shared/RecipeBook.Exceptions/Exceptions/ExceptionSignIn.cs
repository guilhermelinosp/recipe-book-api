using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionSignIn : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionSignIn(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ExceptionSignIn(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
