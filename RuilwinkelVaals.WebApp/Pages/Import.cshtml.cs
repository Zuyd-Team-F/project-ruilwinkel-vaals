using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RuilwinkelVaals.WebApp.Classes;

namespace RuilwinkelVaals.WebApp.Pages
{
    public class ImportModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public IFormFile uploadFile { get; set; }
        public void OnPost()
		{
            Import.readCSV(uploadFile);
            
        }
    }
}
