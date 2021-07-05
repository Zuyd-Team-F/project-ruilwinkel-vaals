using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Bedrijf")]
        public string Business { get; set; }

        [Display(Name = "Rol")]
        public string Role { get; set; }

        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Display(Name = "Geboorte datum")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Straat")]
        public string Street { get; set; }

        [Display(Name = "Huisnummer")]
        public int StreetNumber { get; set; }

        [Display(Name = "Straat toevoeging")]
        public string StreetAdd { get; set; }

        [Display(Name = "Postcode")]
        public string PostalCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Telefoon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Balans")]
        public int Balance { get; set; }

        public bool Blacklist { get; set; }

        [Display(Name = "Foto")]
        public string Image { get; set; }
    }
}