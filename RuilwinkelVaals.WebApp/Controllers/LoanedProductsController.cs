using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;

namespace RuilwinkelVaals.WebApp.Controllers
{
    public class LoanedProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoanedProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoanedProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoanedProducts.Include(l => l.Product).Include(l => l.User);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LoanedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedProduct = await _context.LoanedProducts
                .Include(l => l.Product)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (loanedProduct == null)
            {
                return NotFound();
            }

            return View(loanedProduct);
        }

        // GET: LoanedProducts/Create
        public IActionResult Create()
        {
            var products = _context.Product.ToList();
            List<object> pList = new List<object>();
            foreach (var p in products)
                pList.Add(new
                {
                    Id = p.Id,
                    Name = p.Id + " - " + p.Name
                });

            ViewData["ProductId"] = new SelectList(pList, "Id", "Name");

            var users = _context.Users.ToList();
            List<object> uList = new List<object>();
            foreach (var u in users)
                uList.Add(new
                {
                    Id = u.Id,
                    Name = u.Id + " - " + u.FirstName + " " + u.LastName
                });

            ViewData["UserId"] = new SelectList(uList, "Id", "Name");
            return View();
        }

        // POST: LoanedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,UserId,DateStart,DateEnd")] LoanedProduct loanedProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanedProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", loanedProduct.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "City", loanedProduct.UserId);
            return View(loanedProduct);
        }

        // GET: LoanedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedProduct = await _context.LoanedProducts.FindAsync(id);
            if (loanedProduct == null)
            {
                return NotFound();
            }

            var products = _context.Product.ToList();
            List<object> pList = new List<object>();
            foreach (var p in products)
                pList.Add(new
                {
                    Id = p.Id,
                    Name = p.Id + " - " + p.Name
                });

            ViewData["ProductId"] = new SelectList(pList, "Id", "Name", loanedProduct.ProductId);

            var users = _context.Users.ToList();
            List<object> uList = new List<object>();
            foreach (var u in users)
                uList.Add(new
                {
                    Id = u.Id,
                    Name = u.Id + " - " + u.FirstName + " " + u.LastName
                });

            ViewData["UserId"] = new SelectList(uList, "Id", "Name", loanedProduct.UserId);
            return View(loanedProduct);
        }

        // POST: LoanedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,UserId,DateStart,DateEnd")] LoanedProduct loanedProduct)
        {
            if (id != loanedProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanedProductExists(loanedProduct.Id))
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
            
            return View(loanedProduct);
        }

        // GET: LoanedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanedProduct = await _context.LoanedProducts
                .Include(l => l.Product)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanedProduct == null)
            {
                return NotFound();
            }

            return View(loanedProduct);
        }

        // POST: LoanedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanedProduct = await _context.LoanedProducts.FindAsync(id);
            _context.LoanedProducts.Remove(loanedProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanedProductExists(int id)
        {
            return _context.LoanedProducts.Any(e => e.Id == id);
        }
    }


}
