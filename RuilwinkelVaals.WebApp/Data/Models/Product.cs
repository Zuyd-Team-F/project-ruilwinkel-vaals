using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [Display(Name="Kwaliteit")]
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [MaxLength(24)]
        [Display(Name = "Merk")]
        public String Brand { get; set; }

        [Display(Name = "Beschrijving")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Naam")]
        [MaxLength(56)]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Creditwaarde")]
        public int CreditValue { get; set; }
    }
}
