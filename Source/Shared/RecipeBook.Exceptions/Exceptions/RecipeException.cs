using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class RecipeException : BaseException
{
    public RecipeException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }


    protected RecipeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}