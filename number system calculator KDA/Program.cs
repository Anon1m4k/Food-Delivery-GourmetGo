using Microsoft.EntityFrameworkCore;
using number_system_calculator_KDA.Data;
using number_system_calculator_KDA.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Настройка подключения к базе данных 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryDb")));
builder.Services.AddSignalR(options => {
    options.EnableDetailedErrors = true;
});
builder.Services.AddSingleton<ChatHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();
app.MapHub<ChatHub>("/chatHub");

app.UseAuthorization();

app.MapRazorPages();

app.Run();