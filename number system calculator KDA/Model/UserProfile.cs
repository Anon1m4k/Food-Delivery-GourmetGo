namespace number_system_calculator_KDA.Model
{
    public class UserProfile
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int NumberPhone { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }


    }
}
