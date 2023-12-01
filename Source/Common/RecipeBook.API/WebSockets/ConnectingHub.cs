using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeBook.Application.UseCases.WebSockets.AcceptConnection;
using RecipeBook.Application.UseCases.WebSockets.ConsumerQrCode;
using RecipeBook.Application.UseCases.WebSockets.ProducerQrCode;
using RecipeBook.Application.UseCases.WebSockets.RefuseConnection;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.API.WebSockets;

[Authorize(Policy = "AuthorizationHub")]
[Produces("application/json")]
public class ConnectingHub : Hub
{
    private readonly IAcceptConnectionUseCase _acceptConnection;
    private readonly BroadcastHub _broadcastHub;
    private readonly IConsumerQrCodeUseCase _consumerQrCode;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<ConnectingHub> _hubContext;
    private readonly IProducerQrCodeUseCase _producerQrCode;
    private readonly IRefuseConnectionUseCase _refuseConnection;

    public ConnectingHub(
        IProducerQrCodeUseCase producerQrCode,
        IHttpContextAccessor httpContextAccessor,
        IConsumerQrCodeUseCase consumerQrCode,
        IHubContext<ConnectingHub> hubContext,
        BroadcastHub broadcastHub, IRefuseConnectionUseCase refuseConnection, IAcceptConnectionUseCase acceptConnection)
    {
        _producerQrCode = producerQrCode;
        _httpContextAccessor = httpContextAccessor;
        _consumerQrCode = consumerQrCode;
        _hubContext = hubContext;
        _broadcastHub = broadcastHub;
        _refuseConnection = refuseConnection;
        _acceptConnection = acceptConnection;
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
            var qrCode =
                await _producerQrCode.ExecuteAsync(_httpContextAccessor.HttpContext?.Request.Headers.Authorization
                    .ToString()!);
            _broadcastHub.InitialConnection(_hubContext, qrCode.AccountId.ToString(), Context.ConnectionId);
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
            var response = await _consumerQrCode.ExecuteAsync(codeValue,
                _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString()!);
            var connectionId = _broadcastHub.GetProducerConnectionId(response.AccountId.ToString());
            _broadcastHub.ResetTimeExpired(connectionId);
            _broadcastHub.SetCunsumerConnectionId(response.AccountId.ToString(), Context.ConnectionId);
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

    public async Task RefuseConnection()
    {
        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var usuarioId = await _refuseConnection.ExecuteAsync(token!);

            var connectionId =
                _broadcastHub.RemoveConnectionId(Context.ConnectionId, usuarioId.ToString());

            await Clients.Client(connectionId).SendAsync("OnRefuseConnection");
        }
        catch (WebSocketException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ErrorMessages.ERRO_DESCONHECIDO);
        }
    }

    public async Task AcceptConnection(string accountId)
    {
        try
        {
            var connectionId = await _acceptConnection.ExecuteAsync(accountId,
                _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString()!);

            var connectionIdRemove =
                _broadcastHub.RemoveConnectionId(Context.ConnectionId, connectionId.ToString());

            await Clients.Client(connectionIdRemove).SendAsync("OnAcceptConnection");
        }
        catch (WebSocketException ex)
        {
            await Clients.Caller.SendAsync("Erro", ex.Message);
        }
        catch
        {
            await Clients.Caller.SendAsync("Erro", ErrorMessages.ERRO_DESCONHECIDO);
        }
    }
}