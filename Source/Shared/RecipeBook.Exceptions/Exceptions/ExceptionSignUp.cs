using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionSignUp : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionSignUp(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ExceptionSignUp(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}