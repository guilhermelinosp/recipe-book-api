using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.API.WebSockets;

public sealed class BroadcastHub : Hub
{
    private static IHubContext<ConnectingHub> _hubContext;

    private Action<string> _callbackTimeExpired;
    private string _conusmerConnectionId;
    private string _producerConnectionId;
    private short RemainingTimeSeconds { get; set; }
    private Timer _timer;

    private BroadcastHub(IHubContext<ConnectingHub>? hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        Dictionary = new ConcurrentDictionary<Guid, object>();
    }
    
    private ConcurrentDictionary<Guid, object> Dictionary { get; }

    public void Connection(IHubContext<ConnectingHub>? hubContext, string producerId, string connectionId)
    {
        var broadcastHub = new BroadcastHub(hubContext);

        Dictionary.TryAdd(connectionId, broadcastHub);
        Dictionary.TryAdd(producerId, connectionId);

        broadcastHub.InitialTime(CallbackTimeExpired);
    }

    public string GetConsumerConnectionId(string consumerId)
    {
        if (!Dictionary.TryGetValue(consumerId, out var connectionId))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        return connectionId.ToString()!;
    }

    public string GetProducerConnectionId(string producerId)
    {
        if (!Dictionary.TryGetValue(producerId, out var connectionId))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        return connectionId.ToString()!;
    }

    public string RemoveConsumerConnectionId(string consumerId, string connectionId)
    {
        if (!Dictionary.TryGetValue(connectionId, out _))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        StopTimer();

        Dictionary.TryRemove(connectionId, out _);
        Dictionary.TryRemove(consumerId, out _);

        return GetConsumerConnectionId();
    }

    public string RemoveProducerConnectionId(string producerId, string connectionId)
    {
        if (!Dictionary.TryGetValue(connectionId, out _))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        StopTimer();

        Dictionary.TryRemove(connectionId, out _);
        Dictionary.TryRemove(producerId, out _);

        return GetProducerConnectionId();
    }

    public void SetCunsumerConnectionId(string connectionId)
    {
        _conusmerConnectionId = connectionId;
    }

    private void SetProducerConnectionId(string connectionId)
    {
        _producerConnectionId = connectionId;
    }

    private string GetProducerConnectionId()
    {
        return _producerConnectionId;
    }

    private string GetConsumerConnectionId()
    {
        return _conusmerConnectionId;
    }


    private void CallbackTimeExpired(string connectionId)
    {
        Dictionary.TryRemove(connectionId, out _);
    }

    public void ResetTimeExpired(string connectionId)
    {
        Dictionary.TryGetValue(connectionId, out _);
        ResetTimer();
    }

    private void InitialTime(Action<string> callbackTempoExpirado)
    {
        _callbackTimeExpired = callbackTempoExpirado;
        StartTimer();
    }

    private void StartTimer()
    {
        _timer = new Timer(_ => ElapsedTimer(), null, 0, 1000);
    }

    private void ResetTimer()
    {
        StopTimer();
        StartTimer();
    }

    private void StopTimer()
    {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        _timer.Dispose();
    }

    private void ElapsedTimer()
    {
        lock (_timerLock)
        {
            if (_tempoRestanteSegundos >= 0)
            {
                _hubContext!.Clients.Client(_producerConnectionId)
                    .SendAsync("SetTempoRestante", _tempoRestanteSegundos--);
            }
            else
            {
                StopTimer();
                _callbackTimeExpired(_producerConnectionId);
            }
        }
    }
}