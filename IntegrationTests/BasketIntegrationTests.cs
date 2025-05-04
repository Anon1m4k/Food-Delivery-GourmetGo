using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Model;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class BasketIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public BasketIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase("OrderTestDb"));
                });
            });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task AddToCart_And_CreateOrder_Successfully()
        {
            // Добавление блюда в БД
            var dish = new Dish
            {
                Title = "Burger",
                Structure = "Test Ingredients",
                Gram = 300,
                Price = 10.0f,
                Sum = 10.0f
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Dishs.Add(dish);
                await db.SaveChangesAsync();
            }

            // Добавление в корзину
            var addToCartResponse = await _client.PostAsync($"/UserDishs?handler=AddToCart&id={dish.Id}", null);
            addToCartResponse.EnsureSuccessStatusCode();

            // Оформление заказа
            var orderResponse = await _client.PostAsync("/Basket?handler=Order", null);
            orderResponse.EnsureSuccessStatusCode();

            // Проверка подтверждения заказа
            var confirmationResponse = await _client.GetAsync("/OrderConfirmation");
            Assert.Equal(HttpStatusCode.OK, confirmationResponse.StatusCode);
        }
    }
}