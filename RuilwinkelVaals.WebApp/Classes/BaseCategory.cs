using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Classes
{
    public abstract class BaseCategory
    {        
        public BaseCategory(string name)
        {
            Name = name;
        }

        public BaseCategory() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(24)]
        [Required]
        public virtual string Name { get; set; }
    }
}
