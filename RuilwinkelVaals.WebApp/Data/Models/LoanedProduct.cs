using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public UserData User { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
