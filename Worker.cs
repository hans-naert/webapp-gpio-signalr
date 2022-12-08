using Microsoft.AspNetCore.SignalR;
namespace MyApp.Namespace
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<OutputHub, IOutput> _outputHub;

        public Worker(ILogger<Worker> logger, IHubContext<OutputHub, IOutput> outputHub)
        {
            _logger = logger;
            _outputHub = outputHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                //await _clockHub.Clients.All.ShowTime(DateTime.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

}