using AppointmentAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppointmentAPI.Models;

namespace AppointmentAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Look for any categories.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            // Add sample categories
            var categories = new Category[]
            {
                new Category { Name = "Salon" },
                new Category { Name = "Car Service" },
                new Category { Name = "Healthcare" },
                new Category { Name = "Fitness" },
                new Category { Name = "Beauty" }
            };

            context.Categories.AddRange(categories);

            // Add sample users
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

            // Add sample shops
            var shops = new Shop[]
            {
                new Shop {
                    OwnerId = 2,
                    CategoryId = 1,
                    Name = "Glamour Salon",
                    Description = "Premium hair and beauty services",
                    Location = "123 Main St, City",
                    OpeningHours = "9:00 AM - 6:00 PM, Mon-Fri"
                },
                new Shop {
                    OwnerId = 2,
                    CategoryId = 2,
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