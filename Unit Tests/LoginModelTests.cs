using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace UnitTests
{
    [TestClass]
    public class LoginModelTests
    {
        [TestMethod]
        public void LoginModel_WithClaimsIdentity_SetsProperties()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            var model = new number_system_calculator_KDA.Model.LoginModel
            {
                Username = identity,
                RememberMe = true
            };

            // Assert
            Assert.IsNotNull(model.Username);
            Assert.IsTrue(model.RememberMe);
        }

        [TestMethod]
        public void LoginModel_WithEmptyFields_ShouldBeInvalid()
        {
            // Arrange
            var model = new number_system_calculator_KDA.Model.Login.LoginModel(); // Пустые поля

            // Act
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(2, results.Count); // Ошибки для Email и Password
        }

        [TestMethod]
        public void LoginModel_WithValidData_ShouldBeValid()
        {
            // Arrange
            var model = new number_system_calculator_KDA.Model.Login.LoginModel
            {
                Email = "test@example.com",
                Password = "Qwerty123!"
            };

            // Act
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        } 
    }
}