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

        public int LogId { get; set; }

        public int EmployeeId { get; set; }

        public String ChangeLog { get; set; }
        
        public DateTime ChangeDate { get; set; }
    }
}
