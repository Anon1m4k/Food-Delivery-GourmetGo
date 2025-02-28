using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class loginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public loginModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public required UserProfile login { get; set; }
        public void OnGet()
        {
        }
    }
}
