using RuilwinkelVaals.WebApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Status : BaseCategory
    {
        public Status() { }
        public Status(string name) : base(name)
        { 
        }
    }
}
