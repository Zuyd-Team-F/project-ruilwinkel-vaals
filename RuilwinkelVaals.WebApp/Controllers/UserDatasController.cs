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
    public class UserDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserDatas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserDatas.Include(u => u.BusinessData).Include(u => u.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserDatas
                .Include(u => u.BusinessData)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // GET: UserDatas/Create
        public IActionResult Create()
        {
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: UserDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BusinessDataId,RoleId,Password,FirstName,LastName,DateOfBirth,Street,StreetNumber,StreetAdd,PostalCode,City,Email,Phone,Balance")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email", userData.BusinessDataId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userData.RoleId);
            return View(userData);
        }

        // GET: UserDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserDatas.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email", userData.BusinessDataId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userData.RoleId);
            return View(userData);
        }

        // POST: UserDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BusinessDataId,RoleId,Password,FirstName,LastName,DateOfBirth,Street,StreetNumber,StreetAdd,PostalCode,City,Email,Phone,Balance")] UserData userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.Id))
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
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email", userData.BusinessDataId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userData.RoleId);
            return View(userData);
        }

        // GET: UserDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.UserDatas
                .Include(u => u.BusinessData)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userData = await _context.UserDatas.FindAsync(id);
            _context.UserDatas.Remove(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDataExists(int id)
        {
            return _context.UserDatas.Any(e => e.Id == id);
        }
    }
}
