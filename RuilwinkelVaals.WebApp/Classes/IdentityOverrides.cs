using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.IdentityOverrides
{
    public class UserRole : IdentityUserRole<int>
    {

    }

    public class UserClaim : IdentityUserClaim<int>
    {

    }

    public class UserLogin : IdentityUserLogin<int>
    {

    }

    public class UserToken : IdentityUserToken<int>
    {

    }

    public class UserStore : UserStore<UserData, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {

        }

        public override Task<IdentityResult> CreateAsync(UserData user, CancellationToken cancellationToken = default)
        {
            user.NormalizedEmail ??= user.Email.ToUpper();
            user.NormalizedUserName ??= user.UserName.ToUpper();
            user.SecurityStamp ??= Guid.NewGuid().ToString();
            return base.CreateAsync(user, cancellationToken);
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
