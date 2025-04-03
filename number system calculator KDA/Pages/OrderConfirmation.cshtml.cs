using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace number_system_calculator_KDA.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string OrderId { get; set; }

        public void OnGet()
        {
            
        }
    }

}
