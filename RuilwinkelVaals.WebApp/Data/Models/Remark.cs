using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Remark
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
    }
}
