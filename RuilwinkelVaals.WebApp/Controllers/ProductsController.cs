using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.ViewModels.Products;

namespace RuilwinkelVaals.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = from p in _context.Product.Include(p => p.Category).Include(p => p.Condition).Include(p => p.Status)
                                       select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(s => s.Name.Contains(searchString));
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Condition)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            ProductCreateViewModel model = new();

            model.Categories = new SelectList(_context.Categories, "Id", "Name");
            model.Conditions = new SelectList(_context.Conditions, "Id", "Name");
            model.Statusses = new SelectList(_context.Statuses, "Id", "Name");

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Product product = new()
                {
                    CategoryId = model.CategoryId,
                    ConditionId = model.ConditionId,
                    StatusId = model.StatusId,
                    Description = model.Description,
                    Name = model.Name,
                    Brand = model.Brand,
                    CreditValue = model.CreditValue,
                    Image = uniqueFileName
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Categories = new SelectList(_context.Categories, "Id", "Name");
            model.Conditions = new SelectList(_context.Conditions, "Id", "Name");
            model.Statusses = new SelectList(_context.Statuses, "Id", "Name");

            return View(model);  //deze moeten we pakken voor te vergelijken dit is een object gemaakt met de gegeven input
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", product.ConditionId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", product.StatusId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,ConditionId,StatusId,Brand,Description,Name,CreditValue")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", product.ConditionId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", product.StatusId);
            return View(product);


        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Condition)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
            => await _context.Product.ToArrayAsync();

        private string UploadedFile(ProductCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "img/products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
