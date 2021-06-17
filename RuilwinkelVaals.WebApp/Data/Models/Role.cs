using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Role : IdentityRole<int>
    {
        public Role(string name)
            :base(roleName: name)
        {
            NormalizedName = name.ToUpper();
        }
    }

    public class RoleClaim : IdentityRoleClaim<int>
    {

    }

    public class RoleStore : RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
