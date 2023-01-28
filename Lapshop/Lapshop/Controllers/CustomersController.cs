using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using System.Security.Cryptography;
using System.Text;

namespace Lapshop.Controllers
{
    public class CustomersController : Controller
    {
        private readonly LapshopDbContext _context;

        public CustomersController()
        {
            _context = new LapshopDbContext();
        }


        // GET: /Profile
        public async Task<IActionResult> Profile()
        {
            if (CustomerId() != null)
            {
                // get customer
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.Id == CustomerId());
                
                if (customer == null) return NotFound();
                else return View(customer);
            }
            else return RedirectToAction(nameof(Login));
        }


        // GET: /Registration
        public IActionResult Registration() => View();

        // POST: /Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("FirstName,LastName,Email,Password,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // when customer email Not exist
                if (!await EmailExists(customer.Email))
                {
                    // encrypt password
                    customer.Password = EncryptPassword(customer.Password);
                    // save in database ..
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    // save in session 
                    int customerId = customer.Id;
                    HttpContext.Session.SetInt32("CustomerId", customerId);
                    return Redirect("~/Home");
                }
                else { ViewBag.CustomerExists = "This Email already exist, Please Login";  }
            }
            return View(customer);
        }


        // GET: /Login
        public IActionResult Login() => View();

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] CustomerLogin customer)
        {
            if (ModelState.IsValid)
            {
                if (await ValidateLogin(customer))
                {
                    // get current Customer
                    var currentCustomer = await _context.Customers.
                        Where(c => c.Email.Equals(customer.Email)).FirstOrDefaultAsync();
					// save in Session
                    HttpContext.Session.SetInt32("CustomerId", currentCustomer.Id);
					return Redirect("~/Home");
				}
            }
            return View(customer);
        }


        // GET: /Logout
        public IActionResult Logout() => View(); 

        // POST: /Logout
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public IActionResult LogutConfirmed()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        // check if customer exist 
        private async Task<bool> EmailExists(string email)
        {
            // get reigstered customer by email
            var customer = await _context.Customers
                .Where(c => c.Email.Equals(email)).FirstOrDefaultAsync();

            if (customer != null) return true;
            else return false;
        }

        private async Task<bool> ValidateLogin(CustomerLogin customer) 
        {
            if (await EmailExists(customer.Email))
            {
                // check current customer
                var regCustomer = await _context.Customers
                    .Where(c => c.Email.Equals(customer.Email)).FirstOrDefaultAsync();
                // get registred customer password ..
                var savedPassword = DecryptPassword(regCustomer.Password);

                if (!savedPassword.Equals(customer.Password))
                {
                    ViewBag.ErrorPassword = "Error Email or Password!";
                    return false;
                }
                else return true;
            }
            else {
                ViewBag.ErrorPassword = "This Email is Not exist, please create a new Account!";
                return false; 
            }
        }


        // ******* ☢ Password Encryption ******* //
        private string EncryptPassword(string password)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;

        }

        // ******* ☢ Password Decryption ******* //
        private string DecryptPassword(string password)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    password = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return password;
        }


        private int? CustomerId() 
        {
            return HttpContext.Session.GetInt32("CustomerId");
        }

    }
}
