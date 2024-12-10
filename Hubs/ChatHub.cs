using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
      
         public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        return base.OnDisconnectedAsync(exception);
    }

    public async Task OutputChanged(bool value)
        {
            Console.WriteLine($"OutputChanged: {value}");
            await Clients.All.SendAsync("Output18", value);
        }
    }
}