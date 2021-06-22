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

        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        [Required]
        [MaxLength(128)]
        public String Email { get; set; }

        [Required]
        public int Phone { get; set; }
    }
}
