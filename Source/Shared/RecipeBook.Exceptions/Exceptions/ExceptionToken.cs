using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ExceptionToken : ExceptionBase
{
    public List<string>? ErrorMessages { get; set; }

    public ExceptionToken(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }


    protected ExceptionToken(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}