using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Blacklist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [Display(Name = "Gebruiker")]
        public UserData User { get; set; }

        [Required]
        [Display(Name = "Reden")]
        public String Reason { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
    }
}
