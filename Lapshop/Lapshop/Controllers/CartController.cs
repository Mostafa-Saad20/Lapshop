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
	public class CartController : Controller
	{
		private readonly LapshopDbContext db;

		public CartController()
		{
			db = new LapshopDbContext();
		}

		// GET: Cart/Index
		public async Task<IActionResult> Index()
		{
			if (GetCustomerId() != null)
			{
				int? customerId = GetCustomerId();

				// get whishlist ..
				var cartList = db.CartItems
					.Include(w => w.Customer)
					.Include(w => w.Laptop)
					.Include(w => w.Accessory)
					.Where(w => w.CustomerId == customerId);

				// get cart Total
				decimal? total = 0;
				foreach (var item in cartList) 
				{
					total += item.Price * item.Quantity;
				}
				ViewBag.Total = total;
				
				return View(await cartList.ToListAsync());
			}
			else { return Redirect("~/Customers/Login"); }
		}


		// GET: Cart/AddLaptop/500
		public async Task<IActionResult> AddLaptop(int? id, int? quantity = 1)
		{
			// check customer
			int? customerId = GetCustomerId();
			if (customerId != null)
			{
				if (id != null)
				{
					// find laptop
					var laptop = await db.Laptops
							.Include(l => l.Brand)
							.Include(l => l.Category)
							.Include(l => l.Seller)
							.FirstOrDefaultAsync(l => l.Id == id);

					// add laptop to whishlist
					if (laptop != null)
					{
						// Add laptop when is Not exist
						if (!await LaptopExist(id))
						{
							CartItem item = new()
							{
								CustomerId = customerId,
								LaptopId = laptop.Id,
								Name  = laptop.Name,
								Price = laptop.Price,
								Quantity = quantity,
								Image = laptop.Image
							};
							// save and return 
							db.CartItems.Add(item);
							await db.SaveChangesAsync();
						}
					}
				}
				return RedirectToAction(nameof(Index));
			}
			else { return Redirect("~/Customers/Login"); }
		}

		// GET: Cart/AddAccessory/500
		public async Task<IActionResult> AddAccessory(int? id, int? quantity = 1)
		{
			// check customer
			int? customerId = GetCustomerId();
			if (customerId != null)
			{
				if (id != null)
				{
					// find laptop
					var accessory = await db.Accessories
							.Include(l => l.Brand)
							.Include(l => l.Category)
							.Include(l => l.Seller)
							.FirstOrDefaultAsync(l => l.Id == id);

					// add laptop to whishlist
					if (accessory != null)
					{
						// Add laptop when is Not exist
						if (!await AccessoryExist(id))
						{
							CartItem item = new()
							{
								CustomerId  = customerId,
								AccessoryId = accessory.Id,
								Name  = accessory.Name,
								Price = accessory.Price,
								Quantity = quantity,
								Image = accessory.Image
							};
							// save and return 
							db.CartItems.Add(item);
							await db.SaveChangesAsync();
						}
					}
				}
				return RedirectToAction(nameof(Index));
			}
			else { return Redirect("~/Customers/Login"); }
		}


        // POST: Cart/IncrementQuantity/5
        public async Task<IActionResult> IncrementQuantity(int? id)
        {
            if (id != null)
            {
                var cartItem = await db.CartItems.FindAsync(id);
                if (cartItem != null)
                {
					cartItem.Quantity += 1;
					db.Update(cartItem);
					await db.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Cart/DecrementQuantity/5
        public async Task<IActionResult> DecrementQuantity(int? id)
        {
            if (id != null)
            {
                var cartItem = await db.CartItems.FindAsync(id);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartItem.Quantity > 1 ? cartItem.Quantity - 1 : 1;
                    db.Update(cartItem);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }





        // POST: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				var cartItem = await db.CartItems.FindAsync(id);
				if (cartItem != null)
				{
					db.CartItems.Remove(cartItem);
				}

				await db.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}


		// find Customer Id ..
		private int? GetCustomerId()
		{
			int? customerId = HttpContext.Session.GetInt32("CustomerId");
			if (customerId != null)
			{
				return customerId;
			}
			else return null;
		}

		private async Task<bool> LaptopExist(int? id)
		{
			var laptop = await db.CartItems
				.Where(w => w.LaptopId == id).FirstOrDefaultAsync();

			return laptop != null;
		}

		private async Task<bool> AccessoryExist(int? id)
		{
			var accessory = await db.CartItems
				.Where(w => w.AccessoryId == id).FirstOrDefaultAsync();

			return accessory != null;
		}
	}
}
