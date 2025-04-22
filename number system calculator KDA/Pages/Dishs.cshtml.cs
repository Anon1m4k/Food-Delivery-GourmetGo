using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace number_system_calculator_KDA.Pages
{
    public class DishsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DishsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Dish> Dishs { get; set; }
        public void OnGet()
        {
            Dishs = _context.Dishs.ToList(); 
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
