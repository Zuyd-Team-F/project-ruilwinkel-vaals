using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserDeleteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        public string LastName { get; set; }
    }
}