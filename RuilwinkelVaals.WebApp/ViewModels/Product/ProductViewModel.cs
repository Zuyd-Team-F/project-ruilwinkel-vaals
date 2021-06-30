using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.ViewModels.Product
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Kwaliteit")]
        public int ConditionId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

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

        [Required]
        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}
