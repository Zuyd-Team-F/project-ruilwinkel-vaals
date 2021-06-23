using RuilwinkelVaals.WebApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Category : BaseCategory
    {
        public Category(string name) : base(name)
        {
        }

        [MaxLength(24)]
        public override string Name { get; set; 
    }
}
