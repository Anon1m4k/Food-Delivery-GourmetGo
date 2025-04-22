using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class DishsUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DishsUserModel(ApplicationDbContext context)
        {
            _context = context;
        }
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
                var basketItem = _context.Baskets.FirstOrDefault(b => b.Dish.Id == id);
                if (basketItem != null)
                {
                    basketItem.Quantity++;
                    _context.Baskets.Update(basketItem);
                }
                else
                {
                    Basket bs = new Basket { Dish = dish, Quantity = 1, Price = dish.Price };
                    _context.Baskets.Add(bs);
                }
                _context.SaveChanges(); // Сохраняем изменения в базе данных
            }
            return RedirectToPage();
        }  
    }
}
