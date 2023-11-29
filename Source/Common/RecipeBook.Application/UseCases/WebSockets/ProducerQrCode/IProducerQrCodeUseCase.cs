using RecipeBook.Domain.Dtos.Responses.WebSockets;

namespace RecipeBook.Application.UseCases.WebSockets.ProducerQrCode;

public interface IProducerQrCodeUseCase
{
    Task<ProducerQrCodeResponse> ExecuteAsync(string token);
}