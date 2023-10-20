using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task OutputChanged(bool value)
        {
            Console.WriteLine($"OutputChanged: {value}");
            await Clients.All.SendAsync("Output18", value);
        }
    }
}