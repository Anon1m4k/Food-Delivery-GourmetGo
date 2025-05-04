using number_system_calculator_KDA.Model.AuthApp;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    [TestClass]
    public class RegisterModelTests
    {
        [TestMethod]
        public void RegisterModel_WithMismatchedPasswords_ShouldBeInvalid()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = "test@example.com",
                Password = "123",
                ConfirmPassword = "456"
            };

            // Act
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Пароли не совпадают"));
        }
    }
}