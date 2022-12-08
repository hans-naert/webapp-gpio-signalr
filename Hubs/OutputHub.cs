using Microsoft.AspNetCore.SignalR;

namespace MyApp.Namespace
{
    public class OutputHub : Hub
    {
        public async Task OutputChanged(bool value)
        {
            Console.WriteLine($"OutputChanged: {value}");
            await Clients.All.SendAsync("OutputChanged", value);
        }
    }
}