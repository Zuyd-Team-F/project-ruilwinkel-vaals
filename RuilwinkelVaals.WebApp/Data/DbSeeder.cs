using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data
{
    public static class DbSeeder
    {
        public static void Init(ApplicationDbContext _context)
        {
            using (RoleStore roleStore = new(_context))
            {
                roleStore.CreateAsync(new() { Name = "Admin", NormalizedName = "ADMIN" });
                roleStore.CreateAsync(new() { Name = "Medewerker", NormalizedName = "MEDEWERKER" });
            }

            using (UserStore userStore = new(_context))
            {
                UserData user;

                user = GenerateUser("Admin");
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "ADMIN");

                user = GenerateUser("Medewerker");
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "MEDEWERKER");
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
