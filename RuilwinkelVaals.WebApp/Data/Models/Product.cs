using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int ConditionId { get; set; }

        public int ProductId { get; set; }

        public int StatusId { get; set; }

        public String Brand { get; set; }

        public String Description { get; set; }

        public String Name { get; set; }

        public int CreditValue { get; set; }
    }
}
