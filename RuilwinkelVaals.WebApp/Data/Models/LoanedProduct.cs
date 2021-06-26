﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class LoanedProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "Gebruiker")]
        public UserData User { get; set; }

        [Required]
        [Display(Name = "Start Datum")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Eind Datum")]
        public DateTime DateEnd { get; set; }
    }
}
