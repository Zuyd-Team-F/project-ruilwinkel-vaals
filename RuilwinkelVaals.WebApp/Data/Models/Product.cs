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
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
        
        public int StatusId { get; set; }

        public String Brand { get; set; }

        public String Description { get; set; }

        [Required]
        public String Name { get; set; }

        public int CreditValue { get; set; }
    }
}
