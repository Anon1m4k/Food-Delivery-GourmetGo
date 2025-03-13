using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class DishModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DishModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }
        public void OnGet(int id)
        {
            if (id > 0)
            {
                Dish = _context.Dishs.FirstOrDefault(b => b.Id == id);
            }
            else
            {
                Dish = new Dish() { Title = "", Structure = "", Gram = 0, Price = 0, Sum = 0 };
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Dish.Id == 0)
            {
                _context.Dishs.Add(Dish);
            }
            else
            {
                _context.Dishs.Update(Dish);
            }
            _context.SaveChanges();
            return RedirectToPage("Dishs");
        }
    }
}
