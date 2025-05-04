using number_system_calculator_KDA.Model;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    [TestClass]
    public class DishTests
    {
        [TestMethod]
        public void Dish_WithEmptyTitle_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                Title = "",
                Structure = "Ingredients",
                Gram = 300,
                Price = 15.0f,
                Sum = 15.0f
            };

            // Act
            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Требуется название."));
        }        
    }
}