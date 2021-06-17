﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RuilwinkelVaals.WebApp.Data.Models;
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
        }

        private static async Task SeedRoles()
        {
            using RoleStore roleStore = new(_context);
            foreach (string role in Roles)
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
            foreach (string condition in Conditions)
            {
                await _context.Conditions.AddAsync(new() { Name = condition } );
            }
        }

        private static async Task SeedCategories()
        {
            foreach (string category in Categories)
            {
                await _context.Categories.AddAsync(new() { Name = category });
            }
        }

        private static async Task SeedStatuses()
        {
            foreach (string status in Statuses)
            {
                await _context.Statuses.AddAsync(new() { Name = status });
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
