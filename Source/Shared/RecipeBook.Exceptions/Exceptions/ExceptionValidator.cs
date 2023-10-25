using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionValidator : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionValidator(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ExceptionValidator(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
