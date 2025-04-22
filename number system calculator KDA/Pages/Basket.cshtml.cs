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

        [BindProperty]  
        public Basket Basket { get; set; }

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
        public IActionResult OnPostOrder()
        {
            // Генерация ID заказа
            var orderId = new Random().Next(100000, 999999).ToString();

            // Очистка корзины
            var baskets = _context.Baskets.ToList();
            _context.Baskets.RemoveRange(baskets);
            _context.SaveChanges();

            // Перенаправление на страницу подтверждения с параметром OrderId
            return RedirectToPage("/OrderConfirmation", new { OrderId = orderId });
        }
       
        public IActionResult OnPostUpdateQuantity(int id, int quantity)
        {
            var basketItem = _context.Baskets
                .Include(b => b.Dish)
                .FirstOrDefault(b => b.Id == id);

            if (basketItem != null)
            {
                basketItem.Quantity = quantity;
                basketItem.Price = basketItem.Dish.Price; // Обновляем цену
                _context.SaveChanges();
            }
            return RedirectToPage();
        }


    }
}