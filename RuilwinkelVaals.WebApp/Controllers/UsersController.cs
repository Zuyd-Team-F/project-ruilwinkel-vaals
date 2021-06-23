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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users.Include(u => u.BusinessData).Include(u => u.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.Users
                .Include(u => u.BusinessData)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.Users.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }
            ViewData["BusinessDataId"] = new SelectList(_context.BusinessDatas, "Id", "Email", userData.BusinessDataId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userData.RoleId);
            return View(userData);
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.Users
                .Include(u => u.BusinessData)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userData = await _context.Users.FindAsync(id);
            _context.Users.Remove(userData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Blacklist/5
        public async Task<IActionResult> Blacklist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userData = await _context.Users
                .Include(u => u.BusinessData)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: Users/Blacklist/5
        [HttpPost, ActionName("Blacklist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlacklistConfirmed(int id)
        {
            var userData = await _context.Users.FindAsync(id);
            // Code toevoegen om gebruiker te blacklisten (moet gekeken worden in de database)
            // Soort check toevoegen om ervoor te zorgen dat een admin niet een andere admin kan blacklisten
            userData.Blacklist = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserData>> GetAll()
            => await _context.Users.ToArrayAsync();

    }
}
