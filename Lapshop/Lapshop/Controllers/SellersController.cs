using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Lapshop.Controllers
{
    public class SellersController : Controller
    {
        private readonly LapshopDbContext db;

        public SellersController()
        {
            db = new LapshopDbContext();
        }

        // GET: /Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SellerLogin seller)
        {
            if (ModelState.IsValid)
            {
                if (ValidateLogin(seller))
                {
                    // save seller data in Session
                    var savedSeller = db.Sellers
                        .Where(s => s.Email.Equals(seller.Email)).FirstOrDefault();
                    HttpContext.Session.SetString("SellerId", savedSeller.Id.ToString());
                    HttpContext.Session.SetString("SellerEmail", seller.Email);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(seller);

        }

        // GET: /Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,Password")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                if (ValidateRegister(seller))
                {
                    db.Add(seller);
                    await db.SaveChangesAsync();
                    // save seller data in Session
                    HttpContext.Session.SetString("SellerId", seller.Id.ToString());
                    HttpContext.Session.SetString("SellerEmail", seller.Email);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(seller);
        }

        // GET: /Index
        public async Task<IActionResult> Index() 
        {
            if (HttpContext.Session.GetString("SellerId") != null) 
            {
                int sellerId = int.Parse(HttpContext.Session.GetString("SellerId"));
                // find some stats ..
                int soldLaptopsCount = db.Laptops.Where(l => l.SellerId == sellerId).Count();
                ViewBag.SoldLaptops  = soldLaptopsCount;

                // find some stats ..
                int soldAccessoriesCount = db.Accessories.Where(l => l.SellerId == sellerId).Count();
                ViewBag.SoldAccessories = soldAccessoriesCount;

                // get seller name for dashboard
                Seller? seller = await db.Sellers.FindAsync(sellerId);
                if (seller != null) ViewBag.SellerName = seller.FirstName + " " + seller.LastName;
                
                return View();
            } else return RedirectToAction(nameof(Login));
        }

        // GET: Sellers/Profile
        public async Task<IActionResult> Profile()
        {
            if (HttpContext.Session.GetString("SellerId") != null) 
            {
                int sellerId = int.Parse(HttpContext.Session.GetString("SellerId"));
                var currentSeller = await db.Sellers.FirstOrDefaultAsync(s => s.Id == sellerId);
                if (currentSeller != null)
                {
                    return View(currentSeller);
                }
                else return NotFound();
            }
            else return RedirectToAction(nameof(Login));

        }

        // GET: Sellers/Edit
        public async Task<IActionResult> Edit()
        {
            if (HttpContext.Session.GetString("SellerId") != null)
            {
                int sellerId = int.Parse(HttpContext.Session.GetString("SellerId"));
                var seller = await db.Sellers.FindAsync(sellerId);
                if (seller == null)
                {
                    return NotFound();
                }
                else return View();
            }
            else return RedirectToAction(nameof(Login));
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


        // ********** Validation ********* //
        private bool ValidateRegister(Seller seller)
        {
            // get saved email ..
            var savedEmail = db.Sellers
                .Where(s => s.Email.Equals(seller.Email)).FirstOrDefault();

            if (!Regex.IsMatch(seller.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,12}$"))
            {
                ViewBag.ErrorPass = "Password must be from [6 - 12] characters, " +
                    "and must contain at least One small character, " +
                    "One Capital character and One Digit number. ";
                return false;
            }
            else if (!Regex.IsMatch(seller.FirstName, @"[a-zA-Z\s\-*]+"))
            {
                ViewBag.ErrorFName = "Invalid First Name!";
                return false;
            }
            else if (!Regex.IsMatch(seller.LastName, @"[a-zA-Z\s\-*]+"))
            {
                ViewBag.ErrorLName = "Invalid Last Name!";
                return false;
            }
            else if (!Regex.IsMatch(seller.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ViewBag.ErrorEmail = "Invalid Email Address!";
                return false;
            }
            // when seller email is exist ..
            else if (savedEmail != null)
            {
                ViewBag.ErrorEmail = "This Email is already exist, please Login!";
                return false;
            }
            else {
                seller.Password = EncryptPassword(seller.Password);
                return true; 
            }
        }

        private bool ValidateLogin(SellerLogin seller)
        {
            // get saved email ..
            var savedSeller = db.Sellers
                .Where(s => s.Email.Equals(seller.Email)).FirstOrDefault();

            if (!Regex.IsMatch(seller.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ViewBag.ErrorLoginEmail = "Invalid Email Address!";
                return false;
            }
            // when client email is not exist ..
            else if (savedSeller == null)
            {
                ViewBag.ErrorLoginEmail = "This Account is Not Found, Please Create new One";
                return false;
            }
            // when client exist, check password ..
            else if (savedSeller != null)
            {
                // check hashed password ..
                var savedPassword = DecryptPassword(savedSeller.Password);
                if (!savedPassword.Equals(seller.Password))
                {
                    ViewBag.ErrorLoginPass = "Error Email or Password!";
                    return false;
                }
                else return true;

            }
            else return true;
        }

        private bool SellerExists(int id) => db.Sellers.Any(e => e.Id == id);


    }
}
