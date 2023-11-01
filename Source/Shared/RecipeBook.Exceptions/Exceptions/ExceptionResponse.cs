namespace RecipeBook.Exceptions.Exceptions;

public class ExceptionResponse
{
    public List<string> Mensagens { get; set; }

    public ExceptionResponse(List<string> mensagens)
    {
        Mensagens = mensagens;
    }
}