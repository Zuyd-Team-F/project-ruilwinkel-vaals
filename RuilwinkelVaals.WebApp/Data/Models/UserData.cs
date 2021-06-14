using System;
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
        public String Password { get; set; } //Hashed

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public String Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        public String StreetAdd { get; set; }

        public int PostalCode { get; set; }

        public String City { get; set; }

        public String Email { get; set; }

        public int Phone { get; set; }

        
        public int Balance { get; set; }
    }
}
