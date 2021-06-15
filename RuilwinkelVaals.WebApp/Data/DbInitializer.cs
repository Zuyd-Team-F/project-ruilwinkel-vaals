using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using RuilwinkelVaals.WebApp.Classes;
using Microsoft.AspNetCore.Identity;

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

                foreach(var r in Enum.GetValues(typeof(Constants.Roles)))
                {
                    context.Roles.Add( new() { Name = r.ToString(), NormalizedName = r.ToString().Normalize() } );
                }

                context.Conditions.AddRange(new Condition[] {
                    new() { Name = "Zeergoed" },
                    new() { Name = "Goed" },
                    new() { Name = "Neutraal" },
                    new() { Name = "Slecht" },
                    new() { Name = "Zeerslecht" },
                });

                context.Statuses.AddRange(new Status[] {
                    new() { Name = "Uitgeleend" },
                    new() { Name = "Reparatie" },
                    new() { Name = "Voorradig" },
                    new() { Name = "Kapot" },
                });

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

                /*context.Users.AddRange(new UserData[] {
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 3, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals3@ruilwinkel.nl", Phone = 0451234567, Balance = 0 },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 1, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals1@ruilwinkel.nl", Phone = 0451234567, Balance = 0 },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 2, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals2@ruilwinkel.nl", Phone = 0451234567, Balance = 0 },
                    new () { RoleId = 1, Password = HashEvent.hashPassword("admin"), FirstName = "Ad", LastName = "Min", DateOfBirth = new DateTime(1990, 1, 1), Street = "straatnaam", StreetNumber = 4, StreetAdd = null, PostalCode = "6666AA", City = "Heerlen", Email = "vaals4@ruilwinkel.nl", Phone = 0451234567, Balance = 0 },
                });*/

                context.SaveChanges();
            }
        }
    }
}
