using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class BusinessData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Naam")]
        public String Name { get; set; }

        [Required]
        [MaxLength(128)]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Telefoon")]
        public int Phone { get; set; }
    }
}
