using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class TokenException : BaseException
{
    public List<string>? ErrorMessages { get; set; }

    public TokenException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }


    protected TokenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}