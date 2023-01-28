using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lapshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LapshopDbContext db;

        public OrdersController()
        {
            db = new LapshopDbContext();
        }


        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            if (GetCustomerId() != null) 
            {
                // get Order Details from Cart
                int? customerId  = GetCustomerId();
                var cartList = await db.CartItems
                    .Where(c => c.CustomerId == customerId).ToListAsync();

                // get cart Total
                if (cartList != null)
                {
                    decimal? total = 0;
                    foreach (var item in cartList)
                    {
                        total += item.Price * item.Quantity;
                    }
                    ViewBag.OrderDetails = cartList;
                    ViewBag.Total = total;
                }
            }
            return View();
        }


        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,Address1,Address2,ZIPCode,PaymentMethod,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (GetCustomerId() != null)
                {
                    order.CustomerId = GetCustomerId();
                    db.Add(order);
                    await db.SaveChangesAsync();
                    // move to success or payment
                    return RedirectToAction(nameof(OrderConfirm));
                }
                else return Redirect("~/Customers/Login");
            }
            
            return View(order);
        }


        public ActionResult OrderConfirm() => View();

        private int? GetCustomerId()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId != null)
            {
                return customerId;
            }
            else return null;
        }

    }
}
