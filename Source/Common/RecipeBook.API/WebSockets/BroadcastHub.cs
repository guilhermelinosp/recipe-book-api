using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.API.WebSockets;

public sealed class BroadcastHub
{
    private static readonly Lazy<BroadcastHub> _instance = new(() => new BroadcastHub());

    public BroadcastHub()
    {
        ConcurrentDictionary = new ConcurrentDictionary<string, object>();
    }

    public static BroadcastHub Instance => _instance.Value;

    private ConcurrentDictionary<string, object> ConcurrentDictionary { get; }

    public void InitialConnection(IHubContext<ConnectingHub> hubContext, string producerId, string connectionId)
    {
        var connection = new Connection(hubContext, connectionId);
        ConcurrentDictionary.TryAdd(connectionId, connection);
        ConcurrentDictionary.TryAdd(producerId, connectionId);
        connection.InitialTime(CallbackTimeExpired);
    }

    private string GetConnectionId(string accountId)
    {
        if (!ConcurrentDictionary.TryGetValue(accountId, out var connectionId))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        return connectionId.ToString()!;
    }


    public string GetConsumerConnectionId(string consumerId)
    {
        if (!ConcurrentDictionary.TryGetValue(consumerId, out var connectionId))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        return connectionId.ToString()!;
    }

    public string GetProducerConnectionId(string producerId)
    {
        if (!ConcurrentDictionary.TryGetValue(producerId, out var connectionId))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        return connectionId.ToString()!;
    }

    private void CallbackTimeExpired(string connectionId)
    {
        ConcurrentDictionary.TryRemove(connectionId, out _);
    }

    public void SetCunsumerConnectionId(string producerId, string consumerId)
    {
        var connectionId = GetConnectionId(producerId);
        ConcurrentDictionary.TryGetValue(connectionId, out var connectionObject);
        var connection = connectionObject as Connection;
        connection?.SetCunsumerConnectionId(consumerId);
    }


    public void ResetTimeExpired(string connectionId)
    {
        ConcurrentDictionary.TryGetValue(connectionId, out var connectionObject);
        var connection = connectionObject as Connection;
        connection?.ResetTimer();
    }

    public string RemoveConnectionId(string connectionId, string accountId)
    {
        if (!ConcurrentDictionary.TryGetValue(connectionId, out var connectionObject))
            throw new WebSocketException(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        var connection = connectionObject as Connection;
        connection!.StopTimer();
        ConcurrentDictionary.TryRemove(connectionId, out _);
        ConcurrentDictionary.TryRemove(accountId, out _);
        return connection.GetConsumerConnectionId();
    }
}

public class Connection
{
    private readonly IHubContext<ConnectingHub> _hubContext;
    private readonly string _producerConnectionId;
    private Action<string> _callbackTimeExpired;
    private string _conusmerConnectionId;

    public Connection(IHubContext<ConnectingHub> hubContext, string producerConnectionId)
    {
        _hubContext = hubContext;
        _producerConnectionId = producerConnectionId;
    }

    private Timer Timer { get; set; }
    private short RemainingTimeSeconds { get; set; }

    public void InitialTime(Action<string> callbackTimeExpired)
    {
        _callbackTimeExpired = callbackTimeExpired;
        StartTimer();
    }

    public void ResetTimer()
    {
        StopTimer();
        StartTimer();
    }

    public void SetCunsumerConnectionId(string connectionId)
    {
        _conusmerConnectionId = connectionId;
    }

    public string GetConsumerConnectionId()
    {
        return _conusmerConnectionId;
    }

    public void StopTimer()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    private void StartTimer()
    {
        RemainingTimeSeconds = 60;
        Timer = new Timer(ElapsedTimerCallback!, null, Timeout.Infinite, 1000);
        Timer.Change(0, 1000);
    }

    private void ElapsedTimerCallback(object state)
    {
        if (RemainingTimeSeconds >= 0)
        {
            _hubContext.Clients.Client(_producerConnectionId)
                .SendAsync("SetTempoRestante", RemainingTimeSeconds--);
        }
        else
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            Timer.Dispose();
            _callbackTimeExpired(_producerConnectionId);
        }
    }
}