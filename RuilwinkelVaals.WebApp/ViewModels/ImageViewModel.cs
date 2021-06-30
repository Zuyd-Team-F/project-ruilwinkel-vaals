using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuilwinkelVaals.WebApp.ViewModels
{
    public abstract class ImageViewModel
    {
        [Display(Name = "Foto")]
        public IFormFile Image { get; set; }
    }
}
