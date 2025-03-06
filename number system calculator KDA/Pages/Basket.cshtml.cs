using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class BasketModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BasketModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Basket> Baskets { get; set; }
        public void OnGet()
        {
            Baskets = _context.Baskets.ToList();
        }
    }
}
