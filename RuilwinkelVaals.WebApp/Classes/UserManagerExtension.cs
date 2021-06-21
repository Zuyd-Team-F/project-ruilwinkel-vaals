using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuilwinkelVaals.WebApp.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RuilwinkelVaals.WebApp.Classes
{
    public class UserManagerExtension : UserManager<UserData>
    {
        public UserManagerExtension(IUserStore<UserData> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserData> passwordHasher, IEnumerable<IUserValidator<UserData>> userValidators, IEnumerable<IPasswordValidator<UserData>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserData>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            
        }

        public async Task<string> GetFirstnameAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.FirstName;
        }

        public async Task<IdentityResult> SetFirstnameAsync(UserData currentUser, String firstname)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.FirstName = firstname;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }
    }
}
