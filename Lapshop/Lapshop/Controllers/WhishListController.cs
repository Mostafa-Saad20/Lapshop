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
    public class WhishListController : Controller
    {
        private readonly LapshopDbContext db;

        public WhishListController()
        {
            db = new LapshopDbContext();
        }

        // GET: WhishList/Index
        public async Task<IActionResult> Index()
        {
            if (GetCustomerId() != null)
            {
                int? customerId = GetCustomerId();

                // get whishlist ..
                var whishList = db.WhishListItems
					.Include(w => w.Customer)
				    .Include(w => w.Laptop)
                    .Include(w => w.Accessory)
                    .Where(w => w.CustomerId == customerId);

                return View(await whishList.ToListAsync());
            }
			else { return Redirect("~/Customers/Login"); }
		}


        // GET: WhishList/AddLaptop/500
        public async Task<IActionResult> AddLaptop(int? id)
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
                            WhishListItem item = new()
                            {
								CustomerId = customerId,
                                LaptopId   = laptop.Id,
								ItemName   = laptop.Name,
								ItemPrice  = laptop.Price,
								ItemImage  = laptop.Image
							};
                            // save and return 
                            db.WhishListItems.Add(item);
                            await db.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else { return Redirect("~/Customers/Login"); }
		}

		// GET: WhishList/AddAccessory/500
		public async Task<IActionResult> AddAccessory(int? id)
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
							WhishListItem item = new()
							{
								CustomerId  = customerId,
                                AccessoryId = accessory.Id,
                                ItemName    = accessory.Name,
                                ItemPrice   = accessory.Price,
                                ItemImage   = accessory.Image
							};
							// save and return 
							db.WhishListItems.Add(item);
							await db.SaveChangesAsync();
						}
					}
				}
				return RedirectToAction(nameof(Index));
			}
			else { return Redirect("~/Customers/Login"); }
		}


		// POST: WhishList/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var whishListItem = await db.WhishListItems.FindAsync(id);
                if (whishListItem != null)
                {
                    db.WhishListItems.Remove(whishListItem);
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
            var laptop = await db.WhishListItems
                .Where(w => w.LaptopId == id).FirstOrDefaultAsync();

            return laptop != null;
        }

		private async Task<bool> AccessoryExist(int? id)
		{
			var accessory = await db.WhishListItems
				.Where(w => w.AccessoryId == id).FirstOrDefaultAsync();

			return accessory != null;
		}
	}
}
