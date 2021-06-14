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
        public int Id { get; set; }

        [Required]
        public int LogId { get; set; }
        public ProductLog Log { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public UserData Employee { get; set; }

        public String ChangeLog { get; set; }
        
        public DateTime ChangeDate { get; set; }
    }
}
