using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using SignalRChat.Hubs;

public class GpioHostedService : IHostedService, IDisposable
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly GpioController _gpioController;
    private readonly int _pinNumber;

    public GpioHostedService(IHubContext<ChatHub> hubContext, GpioController gpioController)
    {
        _hubContext = hubContext;
        _gpioController = gpioController;
        _pinNumber = 17; // Example GPIO pin number
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Initialize GPIO pin
        _gpioController.OpenPin(_pinNumber, PinMode.InputPullUp);

        // Register callback for pin value changes
        _gpioController.RegisterCallbackForPinValueChangedEvent(
            _pinNumber,
            PinEventTypes.Rising | PinEventTypes.Falling,
            OnPinValueChanged
        );

        Console.WriteLine("GPIO Hosted Service started.");
        return Task.CompletedTask;
    }

    private void OnPinValueChanged(object sender, PinValueChangedEventArgs args)
    {
        var isHigh = args.ChangeType == PinEventTypes.Rising;

        // Broadcast the event to SignalR clients
        _hubContext.Clients.All.SendAsync("PinValueChanged", _pinNumber, isHigh).Wait();

        Console.WriteLine($"Pin {_pinNumber} changed to {(isHigh ? "High" : "Low")}");
    }

      


    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Clean up GPIO resources
        if (_gpioController.IsPinOpen(_pinNumber))
        {
            _gpioController.ClosePin(_pinNumber);
        }
        Console.WriteLine("GPIO Hosted Service stopped.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _gpioController?.Dispose();
    }
}
