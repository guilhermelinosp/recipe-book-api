namespace RecipeBook.Domain.Dtos.Responses.WebSockets;

public class ProducerQrCodeResponse
{
    public string Code { get; set; }
    public Guid AccountId { get; set; }
}