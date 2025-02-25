using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace number_system_calculator_KDA.Pages
{
    public class MenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MenuModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Menu> menu { get; set; }
        public void OnGet()
        {
            menu = _context.Menu.ToList();
        }
    }
}
