using System.ComponentModel.DataAnnotations;

namespace number_system_calculator_KDA.Model
{
    public class Dish
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется название.")]
        [StringLength(100, ErrorMessage = "Название не может быть длиннее 100 символов.")]
        public required string? Title { get; set; }

        [Required(ErrorMessage = "Требуется состав.")]
        [StringLength(200, ErrorMessage = "Состав не может быть длиннее 200 символов.")]
        public required string? Structure { get; set; }
        public int Gram { get; set; }
        public float Price { get; set; }
        public float Sum { get; set; }
    }       
}
