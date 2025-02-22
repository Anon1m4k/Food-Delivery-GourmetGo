
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
//using StudentLibrary.Model;
namespace number_system_calculator_KDA.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
            Database.Migrate(); // Автоматически применяет миграции и создаёт базу, если её нет
        }
    }
}
