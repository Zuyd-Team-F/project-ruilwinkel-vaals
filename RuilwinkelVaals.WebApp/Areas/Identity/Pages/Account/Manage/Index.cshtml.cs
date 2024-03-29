﻿using System.ComponentModel.DataAnnotations;
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
        private readonly SignInManager<UserData> _signInManager;
        private readonly UserManagerExtension _userManagerExtension;

        public IndexModel(
            SignInManager<UserData> signInManager,
            UserManagerExtension userManagerExtension)
        {
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

            [Display(Name = "Nummer")]
            public int StreetNumber { get; set; }

            [Display(Name = "Toevoeging")]
            public string StreetAdd { get; set; }

            [Display(Name = "Postcode")]
            public string PostalCode { get; set; }

            [Display(Name = "Stad")]
            public string City { get; set; }

            public string Image { get; set; }
        }

        private async Task LoadAsync(UserData user)
        {
            // This line is not necessary?
            // var userName = await _userManagerExtension.GetUserNameAsync(user);
            var phoneNumber = await _userManagerExtension.GetPhoneNumberAsync(user);

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
                PostalCode = user.PostalCode,
                Image = user.Image
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManagerExtension.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManagerExtension.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManagerExtension.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManagerExtension.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManagerExtension.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManagerExtension.SetPhoneNumberAsync(user, Input.PhoneNumber);
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

            var lastname = await _userManagerExtension.GetLastnameAsync(user);
            if (Input.Lastname != lastname)
            {
                var setLastnameResult = await _userManagerExtension.SetLastnameAsync(user, Input.Lastname);
                if (!setLastnameResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een achternaam.";
                    return RedirectToPage();
                }
            }

            var street = await _userManagerExtension.GetStreetAsync(user);
            if (Input.Street != street)
            {
                var setStreetResult = await _userManagerExtension.SetStreetAsync(user, Input.Street);
                if (!setStreetResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een straat.";
                    return RedirectToPage();
                }
            }

            var streetNumber = await _userManagerExtension.GetStreetNumberAsync(user);
            if (Input.StreetNumber != streetNumber)
            {
                var setStreetNumberResult = await _userManagerExtension.SetStreetNumberAsync(user, Input.StreetNumber);
                if (!setStreetNumberResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een straat.";
                    return RedirectToPage();
                }
            }


            var streetAdd = await _userManagerExtension.GetStreetAddAsync(user);
            if (Input.StreetAdd != streetAdd)
            {
                var setStreetAddResult = await _userManagerExtension.SetStreetAddAsync(user, Input.StreetAdd);
                if (!setStreetAddResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een straat.";
                    return RedirectToPage();
                }
            }


            var postalcode = await _userManagerExtension.GetStreetAsync(user);
            if (Input.PostalCode != postalcode)
            {
                var setPostcalcodeResult = await _userManagerExtension.SetPostalCodeAsync(user, Input.PostalCode);
                if (!setPostcalcodeResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een postcode.";
                    return RedirectToPage();
                }
            }

            var city = await _userManagerExtension.GetStreetAsync(user);
            if (Input.City != city)
            {
                var setCityResult = await _userManagerExtension.SetCityAsync(user, Input.City);
                if (!setCityResult.Succeeded)
                {
                    StatusMessage = "Onverwachte fout bij het instellen van een stad.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
