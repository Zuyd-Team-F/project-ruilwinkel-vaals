
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class UserData : IdentityUser<int>
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

    }
} 
