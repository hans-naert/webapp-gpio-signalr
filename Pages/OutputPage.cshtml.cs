using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Device.Gpio;

namespace MyApp.Namespace
{
    public class OutputPageModel : PageModel
    {
        public void OnGet()
        {
        }

        private GpioController controller;
        
        public OutputPageModel(GpioController controller)
        {
            this.controller = controller;
        }

        [BindProperty]        
        public bool Output18 {get; set;}= false;

        public void OnPost()
        {
            Console.WriteLine($"Ouput 18 is {/*Request.Form["*/Output18/*"]*/}");
            //using (var controller = new GpioController())
            {
                //controller.OpenPin(18, PinMode.Output);
                controller.Write(18, Output18 ? PinValue.High : PinValue.Low);
                Thread.Sleep(2000);
            }
        }
    }
}
