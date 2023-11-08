using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionResetPassword : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionResetPassword(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ExceptionResetPassword(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}