using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RuilwinkelVaals.WebApp.Constants;

namespace RuilwinkelVaals.WebApp.Data
{
    public static class DbSeeder
    {
        public static async Task Init(ApplicationDbContext _context)
        {
            using (RoleStore roleStore = new(_context))
            {
                string[] allRoles = (string[])Enum.GetValues(typeof(Roles));

                foreach(string role in allRoles)
                {
                    await roleStore.CreateAsync(new(role));
                }
            }

            using (UserStore userStore = new(_context))
            {
                UserData user;

                user = GenerateUser("Admin");
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "ADMIN");

                user = GenerateUser("Medewerker");
                await userStore.CreateAsync(user);
                await userStore .AddToRoleAsync(user, "MEDEWERKER");
            }            
        }

        private static UserData GenerateUser(string username)
        {
            var email = $"{username.ToLower()}@test.nl";
            UserData user = new()
            {
                FirstName = username,
                LastName = "",
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                City = "Test City",
                PostalCode = "6666TE",
                Street = "Test Avenue",
                StreetNumber = 1,
                Balance = 0                
            };
            user.PasswordHash = new PasswordHasher<UserData>().HashPassword(user, "test");

            return user;
        }
    }
}
