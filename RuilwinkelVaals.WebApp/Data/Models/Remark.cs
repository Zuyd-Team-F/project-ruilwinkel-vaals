using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Remark
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public String Name { get; set; }

        public DateTime Date { get; set; }
    }
}
