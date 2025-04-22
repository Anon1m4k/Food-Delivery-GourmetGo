using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model.AuthApp;
using System.Security.Claims;

namespace number_system_calculator_KDA.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.AuthApp.RegisterModel Input { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            bool isFirstUser = !_context.AuthUsers.Any();

            var user = _context.AuthUsers.FirstOrDefault(u => u.Email == Input.Email);

            if (user == null)
            {
                _context.AuthUsers.Add(new AuthUser { Email = Input.Email, Password = Input.Password, Role = isFirstUser ? "Admin" : "User" });
                await _context.SaveChangesAsync();

                await Authenticate(Input.Email);
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Çהוסü קעמעמ בûכמ");
            return Page();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
