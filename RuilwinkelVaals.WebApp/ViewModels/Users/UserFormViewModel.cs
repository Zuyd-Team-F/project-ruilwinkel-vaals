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

        [Display(Name = "Rol")]
        public int RoleId { get; set; }

        public SelectList Businesses { get; set; }

        public SelectList Roles { get; set; }

        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Display(Name = "Wachtwoord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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
    }
}