using System.ComponentModel.DataAnnotations;

namespace RuilwinkelVaals.WebApp.Classes
{
    public abstract class BaseCategory
    {        
        protected BaseCategory(string name)
        {
            Name = name;
        }

        protected BaseCategory() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(24)]
        [Required]
        public virtual string Name { get; set; }
    }
}
