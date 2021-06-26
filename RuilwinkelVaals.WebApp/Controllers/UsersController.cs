using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Classes;
using RuilwinkelVaals.WebApp.Data;
using RuilwinkelVaals.WebApp.Data.Models;
using RuilwinkelVaals.WebApp.IdentityOverrides;
using RuilwinkelVaals.WebApp.ViewModels.Users;

namespace RuilwinkelVaals.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManagerExtension _userManager;

        public UsersController(ApplicationDbContext context, UserManagerExtension userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<UserIndexViewModel> users = new();

            foreach(var u in _context.Users)
            {
                users.Add(new()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = await _userManager.GetRoleAsync(u)
                });
            }

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var u = await _context.Users.FindAsync(id);
            if (u == null)
            {
                return NotFound();
            }

            UserInfoViewModel user = new()
            {
                Id = u.Id,
                Business = u.BusinessData?.Name,
                Role = await _userManager.GetRoleAsync(u),
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                City = u.City,
                PostalCode = u.PostalCode,
                Street = u.Street,
                StreetAdd = u.StreetAdd,
                StreetNumber = u.StreetNumber,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                Balance = u.Balance,
                Blacklist = u.Blacklist
            };

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            UserFormViewModel blankUser = new()
            {
                Roles = new SelectList(_context.Roles, "Id", "Name"),
                Businesses = new SelectList(_context.BusinessData, "Id", "Name")
            };

            return View(blankUser);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel userData)
        {
            if (ModelState.IsValid)
            {
                UserData user = await CreateUser(userData);

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(Constants.Roles), userData.RoleId - 1).ToUpper());

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userData);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var u = await _context.Users.FindAsync(id);
            if (u == null)
            {
                return NotFound();
            }

            var roleName = await _userManager.GetRoleAsync(u);
            var roleId = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault().Id;
            
            UserFormViewModel user = new()
            {
                Id = u.Id,
                BusinessId = u.BusinessDataId,
                RoleId = roleId,
                Businesses = new SelectList(_context.BusinessData, "Id", "Name", u.BusinessDataId),
                Roles = new SelectList(_context.Roles, "Id", "Name", roleId),
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.PasswordHash,
                City = u.City,
                PostalCode = u.PostalCode,
                Street = u.Street,
                StreetAdd = u.StreetAdd,
                StreetNumber = u.StreetNumber,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                Balance = u.Balance
            };

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserFormViewModel userData)
        {
            if (id != userData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateUser(userData);
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
                //.Include(u => u.Role)
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

            var u = await _context.Users.FindAsync(id);
            if (u == null)
            {
                return NotFound();
            }

            UserInfoViewModel user = new()
            {
                Id = u.Id,
                Business = u.BusinessData?.Name,
                Role = await _userManager.GetRoleAsync(u),
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                City = u.City,
                PostalCode = u.PostalCode,
                Street = u.Street,
                StreetAdd = u.StreetAdd,
                StreetNumber = u.StreetNumber,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                Balance = u.Balance,
                Blacklist = u.Blacklist
            };

            return View(user);
        }

        // POST: Users/Blacklist/5
        [HttpPost, ActionName("Blacklist")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlacklistConfirmed(int id)
        {
            var userData = await _context.Users.FindAsync(id);

            userData.Blacklist = true;

            await _userManager.UpdateAsync(userData);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IEnumerable<UserData>> GetAll()
            => await _context.Users.ToArrayAsync();

        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static T GetDifference<T>(T val1, T val2)
        {
            return !val1.Equals(val2) ? val1 : val2;
        }

        private async Task UpdateUser(UserFormViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            user.FirstName = GetDifference(model.FirstName, user.FirstName);
            user.LastName = GetDifference(model.LastName, user.LastName);
            user.Email = GetDifference(model.Email, user.Email);
            user.UserName = GetDifference(model.Email, user.Email);
            user.City = GetDifference(model.City, user.City);
            user.PostalCode = GetDifference(model.PostalCode, user.PostalCode);
            user.Street = GetDifference(model.Street, user.Street);
            user.StreetNumber = GetDifference(model.StreetNumber, user.StreetNumber);
            user.PhoneNumber = GetDifference(model.PhoneNumber, user.PhoneNumber);
            user.Balance = GetDifference(model.Balance, user.Balance);

            var roleName = await _userManager.GetRoleAsync(user);
            var roleId = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault().Id;

            //Attempt to change role


            await _userManager.UpdateAsync(user);
        }

        private Task<UserData> CreateUser(UserFormViewModel userData)
        {
            throw new NotImplementedException();
        }
    }
}
