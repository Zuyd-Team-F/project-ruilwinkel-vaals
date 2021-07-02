using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin, Medewerker")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserData> _signInManager;
        private readonly UserManager<UserData> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<UserData> userManager,
            SignInManager<UserData> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Voornaam")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Achternaam")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Telefoonnummer")]
            public string Phone { get; set; }

            [Display(Name = "Geboortedatum")]
            public string DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Straat")]
            public string Street { get; set; }

            [Required]
            [Display(Name = "Nummer")]
            public int StreetNumber { get; set; }

          
            [Display(Name = "Toevoeging")]
            public string StreetAdd { get; set; }


            [Required]
            [Display(Name = "Postcode")]
            public string PostalCode { get; set; }

            [Required]
            [Display(Name = "Stad")]
            public string City { get; set; }

            [Display(Name = "Bedrijfsnaam")]
            public string BusinessName { get; set; }
            [Display(Name = "Email adres")]
            public string BusinessEmail { get; set; }
            [Display(Name = "Telefoonnummer")]
            public string BusinessPhone { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} en maximaal {1} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Valideer wachtwoord")]
            [Compare("Password", ErrorMessage = "Dit wachtwoord is niet conform met de vorige.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new UserData {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email, 
                    Email = Input.Email,
                    City = Input.City,
                    PostalCode = Input.PostalCode,
                    Street = Input.Street,
                    StreetNumber = Input.StreetNumber,
                    StreetAdd = Input.StreetAdd
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
