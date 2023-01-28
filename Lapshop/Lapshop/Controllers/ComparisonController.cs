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
    public class ComparisonController : Controller
    {
		private readonly LapshopDbContext db;

		public ComparisonController()
        {
            // database
            db = new LapshopDbContext();
        }


        public async Task<ActionResult> Laptop() 
        {
            // get laptops to compare
            var compareLaptops = await db.Comparisons
                .Where(l => l.UniqueProperty == GetIP()).ToListAsync();
            
            return View(compareLaptops);
		}


        // Add laptops to comparison
        public async Task<ActionResult> AddLaptopToCompare(int? id)
        {
            if (id != null)
            {
                var laptop = await db.Laptops
				    .Include(l => l.Brand)
				    .Include(l => l.Category)
				    .Include(l => l.Seller)
				    .FirstOrDefaultAsync(l => l.Id == id);

                // add it to Comparison list 
                if (laptop != null)
                {
					var compareLaptops = await db.Comparisons
						.Where(l => l.UniqueProperty == GetIP()).ToListAsync();

                    if (compareLaptops.Count < 3)
                    {

                        Comparison comparison = new()
                        {
                            LaptopId = id,
                            Name = laptop.Name,
                            Image = laptop.Image,
                            Price = laptop.Price,
                            RAMSize = laptop.RAMSize,
                            RAMType = laptop.RAMType,
                            ProcessorBrand = laptop.ProcessorBrand,
                            ProcessorType = laptop.ProcessorType,
                            CPU = laptop.CPU,
                            HDD = laptop.HDD,
                            HasSSD = laptop.HasSSD,
                            SSD = laptop.SSD,
                            GPUBrand = laptop.GPUBrand,
                            GPU = laptop.GPU,
                            OS = laptop.OS,
                            DisplaySize = laptop.DisplaySize,
                            Width = laptop.Width,
                            Length = laptop.Length,
                            Weight = laptop.Weight,
                            Color = laptop.Color,
                            UniqueProperty = GetIP(),
                        };

                        // add comparison
                        db.Comparisons.Add(comparison);
                        await db.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Laptop));
        }


        public async Task<ActionResult> RemoveFromComparison(int? id) 
        {
            if (id != null) 
            {
				var compareItem = await db.Comparisons
					.FirstOrDefaultAsync(c => c.Id == id);

                if (compareItem != null)
                {
                    db.Remove(compareItem);
                    await db.SaveChangesAsync();
                }
			}
			
            return RedirectToAction(nameof(Laptop));
		}


		private string GetIP() 
        {
            return HttpContext.Connection.RemoteIpAddress.GetHashCode().ToString();
        }

	}
}
