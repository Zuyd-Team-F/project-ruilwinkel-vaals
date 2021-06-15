using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class UserData : IdentityUser
    {        
        public Guid? BusinessDataId { get; set; }
        public BusinessData BusinessData { get; set; }

        [Required]
        [MaxLength(32)]
        [Display(Name = "Voornaam")]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(32)]
        public String LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(64)]
        public String Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [MaxLength(2)]
        public String StreetAdd { get; set; }

        [Required]
        [MaxLength(7)]
        public String PostalCode { get; set; }

        [Required]
        [MaxLength(32)]
        public String City { get; set; }

        [Required]
        public int Balance { get; set; }
    }
}