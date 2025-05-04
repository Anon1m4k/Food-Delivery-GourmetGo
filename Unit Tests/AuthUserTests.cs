using number_system_calculator_KDA.Model.AuthApp;

namespace UnitTests
{
    [TestClass]
    public class AuthUserTests
    {
        [TestMethod]
        public void AuthUser_WithValidProperties_ShouldSetValuesCorrectly()
        {
            // Arrange
            var user = new AuthUser
            {
                Id = 1,
                Email = "test@example.com",
                Password = "secure",
                Role = "Admin"
            };

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("test@example.com", user.Email);
            Assert.AreEqual("Admin", user.Role);
        }
    }
}