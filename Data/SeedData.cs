using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppointmentAPI.Models;

namespace AppointmentAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // 1. Ensure Database is Created
            context.Database.EnsureCreated();

            // 2. Add Categories (If they don't exist)
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Salon" },
                    new Category { Name = "Car Service" },
                    new Category { Name = "Healthcare" },
                    new Category { Name = "Fitness" },
                    new Category { Name = "Beauty" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges(); // SAVE NOW to generate IDs
            }

            // 3. Add Users (If they don't exist)
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User {
                        Email = "customer@example.com",
                        FullName = "John Doe",
                        Role = "Customer",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
                    },
                    new User {
                        Email = "owner@example.com",
                        FullName = "Jane Smith",
                        Role = "Owner",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges(); // SAVE NOW to generate IDs
            }

            // 4. Add Shops (If they don't exist)
            if (!context.Shops.Any())
            {
                // Retrieve the actual Entities to get the real IDs
                var owner = context.Users.FirstOrDefault(u => u.Role == "Owner");
                var salonCategory = context.Categories.FirstOrDefault(c => c.Name == "Salon");
                var autoCategory = context.Categories.FirstOrDefault(c => c.Name == "Car Service");

                // Only add shops if we found the owner and categories
                if (owner != null && salonCategory != null && autoCategory != null)
                {
                    var shops = new Shop[]
                    {
                        new Shop {
                            OwnerId = owner.Id, // Use REAL ID
                            CategoryId = salonCategory.Id, // Use REAL ID
                            Name = "Glamour Salon",
                            Description = "Premium hair and beauty services",
                            Location = "123 Main St, City",
                            OpeningHours = "9:00 AM - 6:00 PM, Mon-Fri"
                        },
                        new Shop {
                            OwnerId = owner.Id, // Use REAL ID
                            CategoryId = autoCategory.Id, // Use REAL ID
                            Name = "Quick Fix Auto",
                            Description = "Fast and reliable car repairs",
                            Location = "456 Oak Ave, City",
                            OpeningHours = "8:00 AM - 5:00 PM, Mon-Sat"
                        }
                    };
                    context.Shops.AddRange(shops);
                    context.SaveChanges();
                }
            }
        }
    }
}