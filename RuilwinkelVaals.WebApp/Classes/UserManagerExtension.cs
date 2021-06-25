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

        public async Task<string> GetLastnameAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.LastName;
        }
        public async Task<IdentityResult> SetLastnameAsync(UserData currentUser, String lastname)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.LastName = lastname;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<DateTime> GetDateOfBirthAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.DateOfBirth;
        }
        public async Task<IdentityResult> SetDateOfBirthAsync(UserData currentUser, DateTime dateofbirth)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.DateOfBirth = dateofbirth;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<string> GetStreetAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.Street;
        }
        public async Task<IdentityResult> SetStreetAsync(UserData currentUser, String street)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.Street = street;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<int> GetStreetNumberAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.StreetNumber;
        }
        public async Task<IdentityResult> SetStreetNumberAsync(UserData currentUser, int streetnumber)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.StreetNumber = streetnumber;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<string> GetStreetAddAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.StreetAdd;
        }
        public async Task<IdentityResult> SetStreetAddAsync(UserData currentUser, String streetadd)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.StreetAdd = streetadd;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }


        public async Task<string> GetPostalCodeAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.PostalCode;
        }
        public async Task<IdentityResult> SetPostalCodeAsync(UserData currentUser, String postalcode)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.PostalCode = postalcode;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }


        public async Task<string> GetCityAsync(UserData currentUser)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            return user.City;
        }
        public async Task<IdentityResult> SetCityAsync(UserData currentUser, String city)
        {
            var user = await FindByIdAsync(currentUser.Id.ToString());
            user.City = city;
            await UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<string> GetRoleAsync(UserData user)
        {

            return (await base.GetRolesAsync(user)).FirstOrDefault();
        }
    }
}
