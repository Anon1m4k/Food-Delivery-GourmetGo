using number_system_calculator_KDA.Model;

namespace UnitTests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void Basket_Sum_CalculatesCorrectly()
        {
            // Arrange
            var dish = new Dish
            {
                Title = "Test Dish",
                Structure = "Test Ingredients",
                Gram = 300,
                Price = 10.0f,
                Sum = 10.0f
            };
            var basket = new Basket
            {
                Dish = dish,
                Quantity = 5,
                Price = dish.Price
            };

            // Act
            var expectedSum = basket.Quantity * basket.Price;

            // Assert
            Assert.AreEqual(expectedSum, basket.Sum);
        }
    }
}