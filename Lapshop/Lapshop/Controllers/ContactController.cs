using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;

namespace Lapshop.Controllers
{
    public class ContactController : Controller
    {
        private readonly LapshopDbContext db;

        public ContactController()
        {
            db = new LapshopDbContext();
        }

        // GET: /Create
        public IActionResult Create()
        {
            return Redirect("~/Home/Contact");
        }

        
        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Message")] CustomerMessage customerMessage)
        {
            if (ModelState.IsValid)
            {
                db.Add(customerMessage);
                await db.SaveChangesAsync();
                return Redirect("~/Contact/FeedbackConfirm");
            }
            return View(customerMessage);
        }


        public ActionResult FeedbackConfirm() => View();



    }
}
