using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserIndexViewModel
    {        
        public int Id { get; set; }

        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Display(Name = "Rol")]
        public string Role { get; set; }
    }
}
