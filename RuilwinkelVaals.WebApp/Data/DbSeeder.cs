using Microsoft.AspNetCore.Hosting;
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
        public static async Task Init(ApplicationDbContext context, IWebHostEnvironment env, UserData devUser = null)
        {

            await SeedRoles(context);
            await SeedUsers(context);
            await SeedConditions(context);
            await SeedCategories(context);
            await SeedStatuses(context);
            await SeedProducts(context);

            // Seed the DB with a super user if it's a dev environment
            if(env.EnvironmentName == "Development")
            {
                using UserStore userStore = new(context);
                await userStore.CreateAsync(devUser);
                await userStore.AddToRoleAsync(devUser, "ADMIN");
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedProducts(ApplicationDbContext context)
        {
            await context.Product.AddAsync(new() { Name = "test", Brand = "test", CategoryId = 1, ConditionId = 1, CreditValue = 123, StatusId = 1 });

            await context.SaveChangesAsync();
        }

        public static async Task SeedRoles(ApplicationDbContext context)
        {
            using RoleStore roleStore = new(context);
            foreach (string role in GetEnumArray<Roles>())
            {
                await roleStore.CreateAsync(new(role));
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedUsers(ApplicationDbContext context)
        {
            using UserStore userStore = new(context);
            UserData user;

            user = GenerateUser("Admin");
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "ADMIN");

            user = GenerateUser("Medewerker");
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "MEDEWERKER");

            await context.SaveChangesAsync();
        }

        public static async Task SeedConditions(ApplicationDbContext context)
        {
            
            foreach (string condition in GetEnumArray<Conditions>())
            {
                await context.Conditions.AddAsync(new(condition.Replace('_', ' ')));
                await context.SaveChangesAsync();
            }

        }

        public static async Task SeedCategories(ApplicationDbContext context)
        {
            foreach (string category in GetEnumArray<Categories>())
            {
                await context.Categories.AddAsync(new(category));
                await context.SaveChangesAsync();
            }
           
        }

        public static async Task SeedStatuses(ApplicationDbContext context)
        {
            foreach (string status in GetEnumArray<Statuses>())
            {
                await context.Statuses.AddAsync(new(status));
                await context.SaveChangesAsync();
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

        public static LoanedProduct GenerateLoanedProduct(Product p, UserData u)
        {
            LoanedProduct loanedProduct = new()
            {
                Product = p,
                User = u,
                DateStart = DateTime.Now ,
                DateEnd = DateTime.Now
            };
            return loanedProduct;
        }

        private static string[] GetEnumArray<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        public static Status GenerateStatus(string n, int i)
        {
            Status status = new()
            {
                Id = i,
                Name = n
                
                
            };
            return status;
        }
    }
}
