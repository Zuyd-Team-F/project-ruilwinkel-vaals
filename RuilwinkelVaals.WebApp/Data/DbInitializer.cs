using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using RuilwinkelVaals.WebApp.Classes;

namespace RuilwinkelVaals.WebApp.Data
{
    public class DbInitializer
    {
        public static void Init(ApplicationDbContext context)
        {
            //if(context.Database.GetPendingMigrations().Any())
            if(true)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                context.Roles.AddRange(new Role[] {
                    new() { Name = "Admin" },
                    new() { Name = "Medewerker" },
                    new() { Name = "Klant" },
                    new() { Name = "Business" }
                });
                context.SaveChanges();

                context.Conditions.AddRange(new Condition[] {
                    new() { Name = "Zeergoed" },
                    new() { Name = "Goed" },
                    new() { Name = "Neutraal" },
                    new() { Name = "Slecht" },
                    new() { Name = "Zeerslecht" },
                });
                context.SaveChanges();

                context.Statuses.AddRange(new Status[] {
                    new() { Name = "Uitgeleend" },
                    new() { Name = "Reparatie" },
                    new() { Name = "Voorradig" },
                    new() { Name = "Kapot" },
                });
                context.SaveChanges();

                context.Categories.AddRange(new Category[] {
                    new() { Name = "Kleding" },
                    new() { Name = "Boeken" },
                    new() { Name = "Meubilair" },
                    new() { Name = "Huishoudelijk" },
                    new() { Name = "Spellen" },
                    new() { Name = "Decoratie" },
                    new() { Name = "Electronica" },
                    new() { Name = "Antiek" },
                    new() { Name = "Kunst" },
                    new() { Name = "Huisdieren" }
                });
                context.SaveChanges();

                context.Users.AddRange(new UserData[] {
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 3, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals3@ruilwinkel.nl", Phone = 0451234567, Balance = 0, Blacklist = false },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 1, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals1@ruilwinkel.nl", Phone = 0451234567, Balance = 0, Blacklist = false },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 2, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals2@ruilwinkel.nl", Phone = 0451234567, Balance = 0, Blacklist = false },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 4, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals4@ruilwinkel.nl", Phone = 0451234567, Balance = 0, Blacklist = false },
                });
                context.SaveChanges();
            }
        }
    }
}
