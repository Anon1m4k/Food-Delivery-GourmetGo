using number_system_calculator_KDA.Model;

namespace UnitTests
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        public void Author_WithValidProperties_ShouldSetValuesCorrectly()
        {
            // Arrange
            var author = new Author
            {
                Id = 1,
                Name = "Иван Петров"
            };

            // Assert
            Assert.AreEqual(1, author.Id);
            Assert.AreEqual("Иван Петров", author.Name);
        }
    }
}