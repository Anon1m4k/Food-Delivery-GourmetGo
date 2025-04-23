using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;
using number_system_calculator_KDA.Hubs;
using System;

namespace number_system_calculator_KDA.Pages
{
    [Authorize(Roles = "Admin")]
    public class DishModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        public DishModel(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            string action = Dish.Id == 0 ? "add" : "update";

            if (Dish.Id == 0)
            {
                _context.Dishs.Add(Dish);
            }
            else
            {
                _context.Dishs.Update(Dish);
            }

            await _context.SaveChangesAsync();

            // Если это было добавление нового блюда, Dish.Id теперь будет установлен
            var savedDish = Dish;

            // Отправляем сообщение перед редиректом
            await _hubContext.Clients.All.SendAsync("DishChanged", savedDish, action);

            return RedirectToPage("Dishs");
        }
    }
}
