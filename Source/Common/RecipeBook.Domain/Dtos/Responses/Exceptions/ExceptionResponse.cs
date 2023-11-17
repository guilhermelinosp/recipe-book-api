namespace RecipeBook.Domain.Dtos.Responses.Exceptions;

public class ExceptionResponse
{
    public ExceptionResponse(List<string> mensagens)
    {
        Mensagens = mensagens;
    }

    public List<string> Mensagens { get; set; }
}