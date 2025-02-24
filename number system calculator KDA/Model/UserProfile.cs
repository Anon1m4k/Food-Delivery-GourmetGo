using System.ComponentModel.DataAnnotations;

namespace number_system_calculator_KDA.Model
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется имя.")]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее 50 символов.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Требуется фамилия.")]
        [StringLength(50, ErrorMessage = "Фамилия не может быть длиннее 50 символов.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Требуется номер телефона.")]
        [StringLength(18, ErrorMessage = "Номер телефона не может быть длиннее 18 символов.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Требуется электронная почта.")]
        [StringLength(254, ErrorMessage = "Электронная почта не может быть длиннее 254 символов.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Требуется день рождения.")]
        public DateTime DateOfBirth { get; set; }
    }
}
