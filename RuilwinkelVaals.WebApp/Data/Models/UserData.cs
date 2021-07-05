
using Microsoft.AspNetCore.Identity;
using RuilwinkelVaals.WebApp.Data.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class UserData : IdentityUser<int>, IImageModel
    {        
        public int? BusinessDataId { get; set; }
        public BusinessData BusinessData { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Voornaam")]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Achternaam")]
        public String LastName { get; set; }

        [Display(Name = "Geboortedatum")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Straat")]
        public String Street { get; set; }

        [Required]
        [Display(Name = "Huisnummer")]
        public int StreetNumber { get; set; }

        [MaxLength(2)]
        [Display(Name = "Toevoeging")]
        public String StreetAdd { get; set; }

        [Required]
        [MaxLength(7)]
        [Display(Name = "Postcode")]
        public String PostalCode { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Stad")]
        public String City { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public bool Blacklist { get; set; }

        public string Image { get; set; }
    }    
}