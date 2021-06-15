using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            using (RoleStore<IdentityRole> roleStore = new(_context))
            {
                roleStore.CreateAsync(new() { Name = "Admin", NormalizedName = "ADMIN" });
                roleStore.CreateAsync(new() { Name = "Medewerker", NormalizedName = "MEDEWERKER" });
            }

            using (UserStore<UserData> userStore = new(_context))
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
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                EmailConfirmed = true
            };
            user.PasswordHash = new PasswordHasher<UserData>().HashPassword(user, "test");

            return user;
        }
    }
}
