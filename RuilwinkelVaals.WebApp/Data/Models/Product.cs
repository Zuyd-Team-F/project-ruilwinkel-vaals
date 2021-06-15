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
        public Guid Id { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public Guid ConditionId { get; set; }
        public Condition Condition { get; set; }

        [Required]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [MaxLength(24)]
        public String Brand { get; set; }

        public String Description { get; set; }

        [Required]
        [MaxLength(56)]
        public String Name { get; set; }

        [Required]
        public int CreditValue { get; set; }
    }
}
