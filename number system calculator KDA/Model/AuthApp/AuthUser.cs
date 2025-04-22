namespace number_system_calculator_KDA.Model.AuthApp
{
    public class AuthUser
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}