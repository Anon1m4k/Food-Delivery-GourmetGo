using System.ComponentModel.DataAnnotations;

namespace number_system_calculator_KDA.Model
{
    public class Basket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется название.")]
        public required string Title { get; set; }
        public float Price { get; set; }

        [Required(ErrorMessage = "Требуется количество.")]
        [Range(0, 100, ErrorMessage = "Количество должно быть между 0 и 100.")]
        public int Quantity { get; set; }
    }
}
