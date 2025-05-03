using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using number_system_calculator_KDA.Data;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AuthIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Используем InMemory Database
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase("AuthTestDb"));
                });
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task RegisterUser_And_Login_Successfully()
        {
            // Регистрация
            var registerData = new
            {
                Email = "test@example.com",
                Password = "Qwerty123!",
                ConfirmPassword = "Qwerty123!"
            };

            var registerResponse = await _client.PostAsJsonAsync("/Account/Register", registerData);
            registerResponse.EnsureSuccessStatusCode();

            // Вход
            var loginData = new
            {
                Email = "test@example.com",
                Password = "Qwerty123!"
            };

            var loginResponse = await _client.PostAsJsonAsync("/Account/Login", loginData);
            loginResponse.EnsureSuccessStatusCode();

            // Проверка аутентификации
            var profileResponse = await _client.GetAsync("/Account/Profile");
            Assert.Equal(HttpStatusCode.OK, profileResponse.StatusCode);
        }
    }
}