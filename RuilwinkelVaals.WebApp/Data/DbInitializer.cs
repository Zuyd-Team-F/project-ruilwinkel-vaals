using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data
{
    public class DbInitializer
    {
        public static void Init(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
