using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class ProductLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid LogId { get; set; }
        public ProductLog Log { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
        public UserData Employee { get; set; }

        [Required]
        public String ChangeLog { get; set; }

        [Required]
        public DateTime ChangeDate { get; set; }
    }
}
