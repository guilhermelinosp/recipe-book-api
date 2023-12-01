using RecipeBook.Domain.Dtos.Responses.WebSockets;

namespace RecipeBook.Application.UseCases.WebSockets.ConsumerQrCode;

public interface IConsumerQrCodeUseCase
{
    Task<ConsumerQrCodeResponse> ExecuteAsync(string codeValue, string token);
}