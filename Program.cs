using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System.Device.Gpio;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<GpioController>(serviceProvider =>
{
    var controller = new GpioController();
    var hubContext = serviceProvider.GetRequiredService<IHubContext<ChatHub>>();
    controller.OpenPin(18, PinMode.Output);
    controller.OpenPin(17, PinMode.InputPullUp);
    controller.RegisterCallbackForPinValueChangedEvent(
            17,
            PinEventTypes.Rising | PinEventTypes.Falling,
             (sender, args) => {
               
                hubContext.Clients.All.SendAsync("PinValueChanged", 17, args.ChangeType == PinEventTypes.Rising).Wait(); 
                });
     
    Console.WriteLine("GPIO controller created");
    return controller;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();
