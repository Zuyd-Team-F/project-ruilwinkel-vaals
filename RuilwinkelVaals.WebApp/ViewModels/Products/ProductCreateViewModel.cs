using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.ViewModels.Products
{
    public class ProductCreateViewModel : ImageViewModel
    {
        [Required(ErrorMessage = "Kies een categorie")]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kies de kwaliteit van het product")]
        [Display(Name = "Kwaliteit")]
        public int ConditionId { get; set; }

        [Required(ErrorMessage = "Geef de huidige status van het product")]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [MaxLength(24)]
        [Display(Name = "Merk")]
        public String Brand { get; set; }

        [Display(Name = "Beschrijving")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Geef de naam van het product")]
        [Display(Name = "Naam")]
        [MaxLength(56)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Geef de waarde van het product")]
        [Display(Name = "Creditwaarde")]
        public int CreditValue { get; set; }

        public int UserId { get; set; }

        public SelectList Categories { get; set; }
        public SelectList Conditions { get; set; }
        public SelectList Statusses { get; set; }
    }
}