using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class LoanedProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "Gebruiker")]
        public UserData User { get; set; }

        [Required]
        [Display(Name = "Start Datum")]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [Display(Name = "Eind Datum")]
        [DataType(DataType.Date)]
        [DateGreaterThan("DateStart")]
        public DateTime DateEnd { get; set; }
    
        
    }

    public class DateGreaterThan: ValidationAttribute
    {
        private string _startDatePropertyName;
        public DateGreaterThan(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var PropertyInfo = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            var PropertyValue = PropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if((DateTime)value > (DateTime)PropertyValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Einddatum mag niet eerder zijn dan startdatum.");
            }
        }
    }
}
