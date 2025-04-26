using System.Security.Claims;

namespace number_system_calculator_KDA.Model
{
    public class LoginModel
    {
        public ClaimsIdentity? Username { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}
