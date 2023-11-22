using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RecipeBook.API.WebSockets;

[Authorize(Policy = "AccountAuth")]
public class ConnectingHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("SendAction", "Connected", Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.All.SendAsync("SendAction", "Disconnected", Context.ConnectionId);
    }

    public async Task SendQrCode(string connectionId)
    {
        var qrCode = "test";

        await Clients.Client(connectionId).SendAsync("SendQrCode", qrCode);
    }
}