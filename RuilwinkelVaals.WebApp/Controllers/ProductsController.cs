using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RuilwinkelVaals.WebApp.Classes.Services;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.ViewModels.Products;

namespace RuilwinkelVaals.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageHandler _imgHandler;
        private readonly IWebHostEnvironment _env;
        private readonly IToastNotification _toast;

        public ProductsController(
            ApplicationDbContext context, 
            IImageHandler imageHandler, 
            IWebHostEnvironment env, 
            IToastNotification toastNotification)
        {
            _context = context;
            _env = env;
            _imgHandler = imageHandler;
            _toast = toastNotification;
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

            model.Categories = new SelectList(_context.Categories.OrderBy(s => s.Id), "Id", "Name");
            model.Conditions = new SelectList(_context.Conditions.OrderBy(s => s.Id), "Id", "Name");
            model.Statusses = new SelectList(_context.Statuses.OrderBy(s => s.Id), "Id", "Name");

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
                var testUser = DbSeeder.GenerateUser("Naam");
                testUser.Balance = 10;
                ProductCreateViewModel testModel = DbSeeder.GenerateProductView("Chromebook", testUser.Id);
                if (model.Name != testModel.Name)
                {
                    string uniqueFileName = _imgHandler.UploadedFile(model);
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

                    _toast.AddSuccessToastMessage($"Product '{product.Name}' is succesvol opgeslagen!");

                    await EditBalance(model.UserId, product.CreditValue);
                    return RedirectToAction(nameof(Index));
                }

                Product testProduct = new()
                {
                    CategoryId = model.CategoryId,
                    ConditionId = model.ConditionId,
                    StatusId = model.StatusId,
                    Description = model.Description,
                    Name = model.Name,
                    Brand = model.Brand,
                    CreditValue = model.CreditValue
                };

                _context.Add(testProduct);
                await _context.SaveChangesAsync();

                await EditBalance(model.UserId, testProduct.CreditValue);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", model.ConditionId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", model.StatusId);
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
            _imgHandler.RemoveFile(product);
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

        private async Task<bool> EditBalance(int givenUserId, int productValue)
        {
            var user = _context.UserData.Where(u => u.Id == givenUserId).FirstOrDefault();
            int tradedAmount = productValue;
            int currentBalance = user.Balance;
            int newBalance = currentBalance + tradedAmount;
            if (newBalance >= 0)
            {
                user.Balance = newBalance;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
