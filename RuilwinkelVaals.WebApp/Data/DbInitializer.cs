using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data
{
    public class DbInitializer
    {
        public static void Init(ApplicationDbContext context)
        {
            if(context.Database.GetPendingMigrations().Any())
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
            }
        }
    }
}
