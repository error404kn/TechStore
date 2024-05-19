using Microsoft.EntityFrameworkCore;
using TechStore.Data;

namespace TechStore.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TechStoreContext(serviceProvider.GetRequiredService<DbContextOptions<TechStoreContext>>()))
            {
                if(context.Technics.Any()) // Check database contains any technics
                {
                    return; // Database contains technics already
                }

                context.Technics.AddRange(
                   new Technics
                   {
                       ModelName = "Razer Opus X Mercury Edition",
                       Price = 100,
                       Description = "ANC Mode, Ambiend Mode"
                   },
                   new Technics
                   {
                       ModelName = "Razer Opus X Greeb Edition",
                       Price = 120,
                       Description = "ANC Mode, Ambiend Mode"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
