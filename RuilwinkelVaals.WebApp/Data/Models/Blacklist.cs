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
        public UserData User { get; set; }

        [Required]
        public String Reason { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
