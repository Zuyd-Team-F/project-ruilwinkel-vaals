using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class BusinessData
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public int Phone { get; set; }
    }
}
