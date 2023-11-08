using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionForgotPassword : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionForgotPassword(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }


    protected ExceptionForgotPassword(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}