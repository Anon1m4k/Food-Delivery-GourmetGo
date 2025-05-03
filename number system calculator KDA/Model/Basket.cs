using System.ComponentModel.DataAnnotations;

namespace number_system_calculator_KDA.Model
{
    public class Basket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется название.")]
        public required Dish Dish { get; set; }
       
        [Required(ErrorMessage = "Требуется количество.")]
        [Range(0, 100, ErrorMessage = "Количество должно быть между 0 и 100.")]
        public int Quantity
        {
            get
            {
                return quantity;    // возвращаем значение свойства
            }
            set
            {
                quantity = value;
                sum = quantity * price; // устанавливаем новое значение свойства
            }
        }
        public float Price
        {
            get
            {
                return price;    // возвращаем значение свойства
            }
            set
            {
                price = value;
                sum = quantity * price; 
                // устанавливаем новое значение свойства
            }
        }
        public float Sum
        {
            get
            {
                return sum;    // возвращаем значение свойства
            }
            set
            {
                sum = value;   // устанавливаем новое значение свойства
            }
        }
        private float sum;
        private int quantity;
        private float price;
    }
}