using RuilwinkelVaals.WebApp.Classes;
using System.ComponentModel.DataAnnotations;
namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Condition : BaseCategory
    {
        public Condition() { }
        public Condition(string name) : base(name)
        {
        }
    }
}
