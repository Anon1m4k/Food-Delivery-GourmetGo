using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Hubs;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Pages
{
    public class DishsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public DishsModel(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public List<Dish> Dishs { get; set; }

        public void OnGet()
        {
            Dishs = _context.Dishs.ToList(); 
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var dish = _context.Dishs.Find(id);
            if (dish != null)
            {
                _context.Dishs.Remove(dish);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("DishChanged", dish, "delete");
            }
            return RedirectToPage();
        }
    }
}