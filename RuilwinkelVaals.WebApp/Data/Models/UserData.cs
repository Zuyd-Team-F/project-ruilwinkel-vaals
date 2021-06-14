﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }
        
        public int BusinessDataId { get; set; }
        public BusinessData BusinessData { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        [MaxLength(64)]
        public String Password { get; set; } //Hashed

        [Required]
        [MaxLength(32)]
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
        public int PostalCode { get; set; }

        [Required]
        [MaxLength(32)]
        public String City { get; set; }

        [Required]
        [MaxLength(128)]
        public String Email { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public int Balance { get; set; }
    }
}