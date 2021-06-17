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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RuilwinkelVaals.WebApp.Data
{
    public static class DbInitializer
    {
        public static async Task Init(ApplicationDbContext context)
        {
            //if(context.Database.GetPendingMigrations().Any())
            if(true)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                await DbSeeder.Init(context);
                            
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

                await context.SaveChangesAsync();
            }
        }
    }
}
