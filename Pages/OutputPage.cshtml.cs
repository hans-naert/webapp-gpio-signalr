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
            Console.WriteLine($"Output18: {Output18}");
        }

        public void OnGet()
        {
        }
    }
}
