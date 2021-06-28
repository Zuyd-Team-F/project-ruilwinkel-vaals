using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.IdentityOverrides;
using System;
using System.Threading.Tasks;
using static RuilwinkelVaals.WebApp.Constants;

namespace RuilwinkelVaals.WebApp.Data
{
    public static class DbSeeder
    {
        private static ApplicationDbContext _context;

        public static async Task Init(ApplicationDbContext context)
        {
            _context = context;

            await SeedRoles();
            await SeedUsers();
            await SeedConditions();
            await SeedCategories();
            await SeedStatuses();
            await _context.SaveChangesAsync();

            await SeedProducts();

            await _context.SaveChangesAsync();
        }

        private static async Task SeedProducts()
        {
            await _context.Product.AddAsync(new() { Name = "test", Brand = "test", CategoryId = 1, ConditionId = 1, CreditValue = 123, StatusId = 1 });
        }

        private static async Task SeedRoles()
        {
            using RoleStore roleStore = new(_context);
            foreach (string role in GetEnumArray<Roles>())
            {
                await roleStore.CreateAsync(new(role));
            }
        }

        private static async Task SeedUsers()
        {
            using UserStore userStore = new(_context);
            UserData user;

            user = GenerateUser("Admin");
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "ADMIN");

            user = GenerateUser("Medewerker");
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "MEDEWERKER");
        }

        private static async Task SeedConditions()
        {
            foreach (string condition in GetEnumArray<Conditions>())
            {
                await _context.Conditions.AddAsync(new(condition));
            }
        }

        private static async Task SeedCategories()
        {
            foreach (string category in GetEnumArray<Categories>())
            {
                await _context.Categories.AddAsync(new(category));
            }
        }

        private static async Task SeedStatuses()
        {
            foreach (string status in GetEnumArray<Statuses>())
            {
                await _context.Statuses.AddAsync(new(status));
            }
        }

        public static UserData GenerateUser(string username)
        {
            var email = $"{username.ToLower()}@test.nl";
            UserData user = new()
            {
                FirstName = username,
                LastName = "Test",
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                City = "Test City",
                PostalCode = "6666TE",
                Street = "Test Avenue",
                StreetNumber = 1,
                PhoneNumber = "123456789",
                Balance = 0
            };
            user.PasswordHash = new PasswordHasher<UserData>().HashPassword(user, "test");

            return user;
        }

        public static Product GenerateProduct(string v)
        {
            Product product = new()
            {
                Category = new Category("Electronica"),
                Condition = new Condition("Zeerslecht"),
                Status = new Status("Voorradig"),
                Name = v,
                Brand = "Lenovo",
                CreditValue = 10,
                Description = "test test"
            };
            return product;
        }

        private static string[] GetEnumArray<T>()
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
