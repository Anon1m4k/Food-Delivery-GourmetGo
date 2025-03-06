using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class DishsUserModel : PageModel
    {
        public required List<Dish> Dishs { get; set; }
        public void OnGet()
        {
            Dishs = _context.Dishs.ToList();
        }
        public IActionResult OnPostAddToCart(int id)
        {
            
            var dish = _context.Dishs.Find(id);
            if (dish != null)
            {
                
            }

            return RedirectToPage();
        }
        private readonly ApplicationDbContext _context; 

        public DishsUserModel(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
