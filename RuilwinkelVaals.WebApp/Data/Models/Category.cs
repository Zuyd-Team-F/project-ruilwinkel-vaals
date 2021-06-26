using RuilwinkelVaals.WebApp.Classes;
using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Category : BaseCategory
    {
        public Category() { }
        public Category(string name) : base(name)
        {
        }
    }
}
