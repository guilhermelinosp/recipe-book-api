using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeBook.Application.UseCases.WebSockets.ConsumerQrCode;
using RecipeBook.Application.UseCases.WebSockets.ProducerQrCode;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.API.WebSockets;

[Authorize(Policy = "AuthorizationHub")]
[Produces("application/json")]
public class ConnectingHub : Hub
{
    private readonly BroadcastHub _broadcastHub;
    private readonly IHubContext<ConnectingHub> _connectionHubContext;
    private readonly IConsumerQrCodeUseCase _consumerQrCode;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<ConnectingHub>? _hubContext;
    private readonly IProducerQrCodeUseCase _producerQrCode;

    public ConnectingHub(
        IProducerQrCodeUseCase producerQrCode,
        IHttpContextAccessor httpContextAccessor,
        IConsumerQrCodeUseCase consumerQrCode,
        IHubContext<ConnectingHub> hubContext,
        BroadcastHub broadcastHub, IHubContext<ConnectingHub> connectionHubContext)
    {
        _producerQrCode = producerQrCode;
        _httpContextAccessor = httpContextAccessor;
        _consumerQrCode = consumerQrCode;
        _hubContext = hubContext;
        _broadcastHub = broadcastHub;
        _connectionHubContext = connectionHubContext;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task ProducerQrCode()
    {
        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var qrCode = await _producerQrCode.ExecuteAsync(token!);

            _broadcastHub.Connection(_hubContext, qrCode.AccountId.ToString(), Context.ConnectionId);

            await Clients.Caller.SendAsync("ResponseProducerQrCode", qrCode);
        }
        catch (WebSocketException ex)
        {
            await Clients.Caller.SendAsync("Error", ex.ErrorMessages);
        }
        catch

        {
            await Clients.Caller.SendAsync("Error", ErrorMessages.ERRO_DESCONHECIDO);
            throw;
        }
    }

    public async Task ConsumerQrCode(string codeValue)
    {
        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var response = await _consumerQrCode.ExecuteAsync(token!, codeValue);

            var connectionId = _broadcastHub.GetProducerConnectionId(response.AccountId.ToString());

            _broadcastHub.ResetTimeExpired(connectionId);

            _broadcastHub.SetCunsumerConnectionId(Context.ConnectionId);

            await Clients.Client(connectionId).SendAsync("ResponseConsumerQrCode", response);
        }
        catch (WebSocketException ex)
        {
            await Clients.Caller.SendAsync("Error", ex.ErrorMessages);
        }
        catch
        {
            await Clients.Caller.SendAsync("Error", ErrorMessages.ERRO_DESCONHECIDO);
            throw;
        }
    }
}