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

        public int BusinessId { get; set; }

        public int RoleId { get; set; }

        public String Password { get; set; } //Hashed

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public DateTime Birthday { get; set; }

        public String Street { get; set; }

        public int StreetNumber { get; set; }

        public String StreetAdd { get; set; }

        public int PostalCode { get; set; }

        public String City { get; set; }

        public String Email { get; set; }

        public int Phone { get; set; }

        public int Balance { get; set; }
    }
}
