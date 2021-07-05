using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NToastNotify;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Classes.Services;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.ViewModels.Interfaces;

namespace RuilwinkelVaals.WebApp.Areas.Identity.Pages.Account.Manage
{
    public class ChangeProfilePhotoModel : PageModel
    {
        private readonly UserManagerExtension _userManager;
        private readonly SignInManager<UserData> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IToastNotification _toast;
        private readonly IImageHandler _imgHandler;

        public ChangeProfilePhotoModel(
            UserManagerExtension userManager,
            SignInManager<UserData> signInManager,
            ILogger<ChangePasswordModel> logger,
            IToastNotification toast,
            IImageHandler imageHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _toast = toast;
            _imgHandler = imageHandler;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : IImageViewModel
        {
            public string CurrentImage { get; set; }

            [Required]
            public IFormFile Image { get; set; }
        }

        private void Load(UserData user)
        {
            Input = new()
            {
                CurrentImage = user.Image
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
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
                return Page();
            }

            _imgHandler.RemoveFile(user, Constants.ImageModels.Users);
            var imgId = _imgHandler.UploadedFile(Input, Constants.ImageModels.Users);

            user.Image = imgId;
            await _userManager.UpdateAsync(user);

            _toast.AddSuccessToastMessage("Uw profiel foto is succesvol veranderd!");
            return RedirectToPage();
        }
    }
}
