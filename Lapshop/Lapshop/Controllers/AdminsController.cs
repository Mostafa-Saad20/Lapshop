using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lapshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Controllers
{
    public class AdminsController : Controller
    {
        private readonly LapshopDbContext db;

        public AdminsController() {
            db = new LapshopDbContext();
        }

        // GET: /Login
        public IActionResult Login() => View();

        // POST: /Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLogin admin) 
        {
            if (ModelState.IsValid) 
            {
                // check registred Admin ..
                var registeredAdmin = await db.Admins.Where(a => a.Email.Equals(admin.Email)).FirstOrDefaultAsync();
                if (registeredAdmin != null) 
                {
                    // check password ..
                    if (registeredAdmin.Password.Equals(admin.Password))
                    {
                        // save session 
                        HttpContext.Session.SetString("AdminRegister", "Yes");
                        HttpContext.Session.SetString("AdminEmail", admin.Email);
                        return RedirectToAction(nameof(Index));
                    }
                    else { ViewBag.ErrorAdmin = "Error Email or Password!"; }
                }
                else { ViewBag.ErrorAdmin = "Unauthorized Access!"; }
            }
            return View(admin);  
        }

        // GET: /Index
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {
                // get some stats
                ViewBag.regCustomers  = await db.Customers.CountAsync();
                ViewBag.totalOrders   = await db.Orders.CountAsync();
                ViewBag.lapsAvailable = await db.Laptops.CountAsync();
                ViewBag.accAvailable  = await db.Accessories.CountAsync();
                ViewBag.todayOrders   = await db.Orders
                    .Where(o => o.Date == DateTime.Today.Date).CountAsync();
                ViewBag.feedback = await db.CustomerMessages.CountAsync();

                return View();
            }
            else return RedirectToAction(nameof(Login));
        }

        // GET: /AllAdmins
        public async Task<IActionResult> AllAdmins()
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {
                ViewBag.CurrentAdminEmail = HttpContext.Session.GetString("AdminEmail");
                return View(await db.Admins.ToListAsync());
            }
            else return RedirectToAction(nameof(Login));
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
                return View();
            else return RedirectToAction(nameof(Login));
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (ValidateAdmin(admin))
                {
                    db.Add(admin);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(AllAdmins));
                }
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {
                if (id == null || db.Admins == null) return NotFound();
                
                var admin = await db.Admins.FindAsync(id);
                if (admin == null)
                {
                    return NotFound();
                }
                else return View();
            }
            else return RedirectToAction(nameof(Login));
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEdit admin)
        {
            if (id != admin.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // get old admin ..
                    var oldAdmin = await db.Admins.FirstOrDefaultAsync(a => a.Id == id);
                    if (admin.CurrentPassword.Equals(oldAdmin.Password)) 
                    {
                        oldAdmin.Name = admin.Name;
                        oldAdmin.Email = admin.Email;
                        oldAdmin.Password = admin.NewPassword;
                        await db.SaveChangesAsync();
                        return RedirectToAction(nameof(AllAdmins));
                    }
                    else ViewBag.ErrorPassword = "Password is Not Correct!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(admin);
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {
                if (id == null || db.Admins == null)
                {
                    return NotFound();
                }

                var admin = await db.Admins.FirstOrDefaultAsync(m => m.Id == id);
                if (admin == null)
                {
                    return NotFound();
                }

                return View(admin);
            }
            else return RedirectToAction(nameof(Login));
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Admins == null)
            {
                return Problem("Entity set 'LapshopDbContext.Admins'  is null.");
            }
            var admin = await db.Admins.FindAsync(id);
            // check if admin exist and NOT current admin
            if (admin != null && !admin.Email.Equals(HttpContext.Session.GetString("AdminEmail")))
            {
                db.Admins.Remove(admin);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(AllAdmins));
        }

        private bool AdminExists(int id)
        {
            return db.Admins.Any(e => e.Id == id);
        }

        private bool ValidateAdmin(Admin admin) 
        {
            var existedAdmin = db.Admins
                .Where(a => a.Email.Equals(admin.Email)).FirstOrDefault();

            if (existedAdmin != null)
            {
                ViewBag.AdminEmailExist = "Email already exist!";
                return false;
            }
            else if (!admin.ConfirmPassword.Equals(admin.Password)) 
            {
                ViewBag.AdminErrorConfirm = "Password doesn't Match!";
                return false;
            }
            else return true;
        }


        public async Task<ActionResult> Feedback()
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {

                var feedback = await db.CustomerMessages.ToListAsync();
                return View(feedback);
            }
            return RedirectToAction(nameof(Login));
        }



        public ActionResult Logout() 
        {
            if (HttpContext.Session.GetString("AdminRegister") != null)
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
