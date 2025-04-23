using number_system_calculator_KDA.Model;
using Microsoft.AspNetCore.SignalR;

namespace number_system_calculator_KDA.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NotifyDishChanged(Dish dish, string action)
        {
            await Clients.All.SendAsync("DishChanged", dish, action);
        }
    }
}
