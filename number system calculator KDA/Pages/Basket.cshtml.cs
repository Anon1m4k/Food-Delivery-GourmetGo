using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            Baskets = _context.Baskets.Include(c => c.Dish).ToList();
        }
        public IActionResult OnPostDelete(int id)
        {
            var dish = _context.Dishs.Find(id);
            if (dish != null)
            {
                _context.Dishs.Remove(dish);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
    }
}
