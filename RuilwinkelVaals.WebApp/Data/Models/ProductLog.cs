using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class ProductLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public UserData Employee { get; set; }

        [Required]
        [Display(Name = "Log")]
        public String ChangeLog { get; set; }

        [Required]
        [Display(Name = "Verander Datum")]
        public DateTime ChangeDate { get; set; }
    }
}
