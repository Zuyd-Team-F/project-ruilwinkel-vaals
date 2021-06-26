using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.ViewModels.Users
{
    public class UserFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Bedrijf")]
        public int? BusinessId { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int RoleId { get; set; }

        public SelectList Businesses { get; set; }

        public SelectList Roles { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} en maximaal {1} karakters lang zijn.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Geboorte datum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Straat")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Huisnummer")]
        public int StreetNumber { get; set; }

        [Display(Name = "Straat toevoeging")]
        public string StreetAdd { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Stad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Telefoon")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Balans")]
        public int Balance { get; set; }
    }
}