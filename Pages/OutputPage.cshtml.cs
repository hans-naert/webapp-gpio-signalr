using System.Device.Gpio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class OutputPageModel : PageModel
    {

        [BindProperty]
        public bool Output18 { get; set; }

        public void OnPost()
        {
            _gpioController.Write(18, Output18 ? PinValue.High : PinValue.Low);
            Console.WriteLine($"Output18: {Output18}");
        }

        public void OnGet()
        {
        }

        private GpioController _gpioController;
        public OutputPageModel(GpioController gpioController)
        {
            _gpioController = gpioController;
        }
        
    }
}
