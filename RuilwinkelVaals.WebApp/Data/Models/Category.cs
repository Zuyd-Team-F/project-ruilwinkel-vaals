using RuilwinkelVaals.WebApp.Classes;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Category : BaseCategory
    {
        public Category(string name) : base(name)
        {
        }

        [MaxLength(24)]
        public override string Name { get; set; }
    }
}
