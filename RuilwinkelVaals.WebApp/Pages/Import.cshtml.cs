using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Data;

namespace RuilwinkelVaals.WebApp.Pages
{
    public class ImportModel : PageModel
    {
        public ImportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string feedback { get; set; }

        private readonly ApplicationDbContext _context;
        public void OnGet()
        {
        }

        [BindProperty]
        public IFormFile uploadFile { get; set; }
        public void OnPost()
		{
            if (uploadFile != null)
            {
                List<string> feedbackList = Import.readCSV(uploadFile, _context);
                this.feedback = string.Join("\n", feedbackList); ;
            }
			else
			{
                this.feedback = "Upload een bestand.";
			}


        }
    }
}
