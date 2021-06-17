using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class UserData : IdentityUser<int>
    {        
        public int? BusinessDataId { get; set; }
        public BusinessData BusinessData { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Voornaam")]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        public String LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(64)]
        public String Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [MaxLength(2)]
        public String StreetAdd { get; set; }

        [Required]
        [MaxLength(7)]
        public String PostalCode { get; set; }

        [Required]
        [MaxLength(32)]
        public String City { get; set; }

        [Required]
        public int Balance { get; set; }
    }

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
}