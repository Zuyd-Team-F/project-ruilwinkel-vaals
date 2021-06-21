using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<UserData> _userManager;
        private readonly SignInManager<UserData> _signInManager;
        private readonly UserManagerExtension _userManagerExtension;

        public IndexModel(
            UserManager<UserData> userManager,
            SignInManager<UserData> signInManager,
            UserManagerExtension userManagerExtension)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManagerExtension = userManagerExtension;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Telefoon nummer")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Voornaam")]
            public string Firstname { get; set; }

            [Display(Name = "Achternaam")]
            public string Lastname { get; set; }

            [Display(Name = "Geboortedatum")]
            public string DateOfBirth { get; set; }

            [Display(Name = "Straat")]
            public string Street { get; set; }

            [Display(Name = "Straat Nummer")]
            public int StreetNumber { get; set; }

            [Display(Name = "Toevoeging")]
            public string StreetAdd { get; set; }

            [Display(Name = "Postcode")]
            public string PostalCode { get; set; }

            [Display(Name = "Stad")]
            public string City { get; set; }
        }

        private async Task LoadAsync(UserData user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                DateOfBirth = user.DateOfBirth.Day + "-" + user.DateOfBirth.Month + "-" + user.DateOfBirth.Year,
                Street = user.Street,
                StreetNumber = user.StreetNumber,
                StreetAdd = user.StreetAdd,
                City = user.City,
                PostalCode = user.PostalCode
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een telefoonnummer.";
                    return RedirectToPage();
                }
            }

            var firstname = await _userManagerExtension.GetFirstnameAsync(user);
            if (Input.Firstname != firstname)
            {
                var setFirstnameResult = await _userManagerExtension.SetFirstnameAsync(user, Input.Firstname);
                if (!setFirstnameResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een voornaam.";
                    return RedirectToPage();
                }
            }

            var lastname = await _userManagerExtension.GetFirstnameAsync(user);
            if (Input.Lastname != lastname)
            {
                var setFirstnameResult = await _userManagerExtension.SetFirstnameAsync(user, Input.Lastname);
                if (!setFirstnameResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een voornaam.";
                    return RedirectToPage();
                }
            }

            var street = await _userManagerExtension.GetFirstnameAsync(user);
            if (Input.Street != street)
            {
                var setFirstnameResult = await _userManagerExtension.SetFirstnameAsync(user, Input.Street);
                if (!setFirstnameResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een voornaam.";
                    return RedirectToPage();
                }
            }  

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
