using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Classes
{
    public abstract class BaseCategory
    {        
        public BaseCategory(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }
    }
}
