using Microsoft.AspNetCore.Http;
using RuilwinkelVaals.WebApp.Classes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Product : IImageModel
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

        [Display(Name = "Foto")]
        public string Image { get; set; }
    }
}
