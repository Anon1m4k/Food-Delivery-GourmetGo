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
            var basketItem = _context.Baskets.Find(id);
            if (basketItem != null)
            {
                _context.Baskets.Remove(basketItem);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
        public IActionResult OnPostClear()
        {
            var baskets = _context.Baskets.ToList();
            _context.Baskets.RemoveRange(baskets);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}