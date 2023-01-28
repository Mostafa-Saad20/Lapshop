using Lapshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lapshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly LapshopDbContext _db;
        
        public HomeController()
        {
            _db = new LapshopDbContext();
        }


        // GET: /Home
        public async Task<IActionResult> Index() 
        {
            // get all categories ..
            var categories = await _db.LapCategories.ToListAsync();
            if (categories != null) 
            { 
                ViewBag.Categories = categories; 
            }

			// get latest products ..
			var latestProducts = await _db.Laptops.Take(4).ToListAsync();
			ViewBag.LatestProducts = latestProducts;

            return View();
        }


        // GET: /Accessories
        public async Task<IActionResult> Accessories(int? pageNumber, string searchTxt,
            string? sortOrder, decimal? minPrice, decimal? maxPrice, int? category) 
        {
            // for paging routes
			ViewData["MinPrice"]  = minPrice;
			ViewData["MaxPrice"]  = maxPrice;
			ViewData["SearchTxt"] = searchTxt;
			ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentCategory"] = category;

			// get accessories ..
			var accessory = from a in _db.Accessories select a;
            
            // 1) Search ..
            if (!string.IsNullOrEmpty(searchTxt)) 
            {
                // search for name
                accessory = accessory.Where(a => a.Name.Contains(searchTxt));
            }

            // 2) Sorting ..
            if (sortOrder == "price")
            {
                // sort by price
                accessory = accessory.OrderBy(a => a.Price);
            }
            else { //sort by Date (latest to oldest)
                accessory = accessory.OrderByDescending(a => a.SoldAt); 
            }

            // 3) Filter ..
            if (category != null) 
            {
                // filter by Category Id ..
                accessory = accessory.Where(a => a.CategoryId == category);
            }
            if(minPrice != null && maxPrice != null) 
            {
				// filter by price ..
				accessory = accessory.Where(a => a.Price <= maxPrice && a.Price >= minPrice)
                    .OrderBy(a => a.Price);
            }
			if (minPrice != null && maxPrice != null && category != null)
			{
				// filter by price and category Id ..
				accessory = accessory.Where(a => a.Price <= maxPrice && a.Price >= minPrice && 
                    a.CategoryId == category).OrderBy(a => a.Price);
			}

			// 4) Paging ..
			int pageSize  = 6;
			// display paged list of Accessories
			var accessories = await PaginatedList<Accessory>
                .CreateAsync(accessory.AsNoTracking(), pageNumber ?? 1, pageSize);
            
            // get categories list for View
			var categories = await _db.AccCategories.ToListAsync();
			ViewBag.Categories = categories;

			return View(accessories);	
		}

		// GET: /Laptops
		public async Task<IActionResult> Laptops(int? pageNumber, string searchTxt,
			string? sortOrder, decimal? minPrice, decimal? maxPrice, int? category, int? brand)
		{
			// for paging routes
			ViewData["MinPrice"] = minPrice;
			ViewData["MaxPrice"] = maxPrice;
			ViewData["SearchTxt"] = searchTxt;
			ViewData["CurrentSort"] = sortOrder;
			ViewData["CurrentCategory"] = category;
			ViewData["CurrentBrand"] = brand;

			// get accessories ..
			var laptop = from l in _db.Laptops select l;

			// 1) Search ..
			if (!string.IsNullOrEmpty(searchTxt))
			{
				// search for name
				laptop = laptop.Where(a => a.Name.Contains(searchTxt));
			}

			// 2) Sorting ..
			if (sortOrder == "price")
			{
				// sort by price
				laptop = laptop.OrderBy(a => a.Price);
			}
			else
			{ //sort by Date (latest to oldest)
				laptop = laptop.OrderByDescending(a => a.SoldAt);
			}

			// 3) Filter ..
			if (category != null)
			{
				// filter by Category Id ..
				laptop = laptop.Where(a => a.CategoryId == category);
			}
			if (brand != null)
			{
				// filter by Brand Id ..
				laptop = laptop.Where(a => a.BrandId == brand);
			}
			if (minPrice != null && maxPrice != null)
			{
				// filter by price ..
				laptop = laptop.Where(a => a.Price <= maxPrice && a.Price >= minPrice)
					.OrderBy(a => a.Price);
			}
			if (minPrice != null && maxPrice != null && category != null)
			{
				// filter by price and category Id ..
				laptop = laptop.Where(a => a.Price <= maxPrice && a.Price >= minPrice &&
					a.CategoryId == category).OrderBy(a => a.Price);
			}

			// 4) Paging ..
			int pageSize = 6;
			// display paged list of Accessories
			var laptops = await PaginatedList<Laptop>
				.CreateAsync(laptop.AsNoTracking(), pageNumber ?? 1, pageSize);

			// get categories list for View
			var categories = await _db.LapCategories.ToListAsync();
			ViewBag.Categories = categories;
			// get categories list for View
			var brands = await _db.LapBrands.ToListAsync();
			ViewBag.Brands = brands;

			return View(laptops);
		}

		
		// GET: /LaptopDetails/100
		public async Task<IActionResult> LaptopDetails(int? id) 
		{
			if (id != null)
			{
				// get reviews for Product View
				var reviews = await _db.LaptopReviews.Where(f => f.LapId == id).ToListAsync();
				if (reviews != null)
				{
					ViewBag.Reviews = reviews;
					// get the count of each Rate (1..5)
					ViewBag.FiveStarsCount  = await _db.LaptopReviews.Where(f => f.LapId == id && f.Rate == 5).CountAsync();
					ViewBag.FourStarsCount  = await _db.LaptopReviews.Where(f => f.LapId == id && f.Rate == 4).CountAsync();
					ViewBag.ThreeStarsCount = await _db.LaptopReviews.Where(f => f.LapId == id && f.Rate == 3).CountAsync();
					ViewBag.TwoStarsCount   = await _db.LaptopReviews.Where(f => f.LapId == id && f.Rate == 2).CountAsync();
					ViewBag.OneStarCount    = await _db.LaptopReviews.Where(f => f.LapId == id && f.Rate == 1).CountAsync();
					// get overall Rating
					decimal? overallRating = 0;
					foreach (var review in reviews)
					{
						overallRating += review.Rate;
					}
					if (reviews.Count != 0)
					{
						overallRating /= reviews.Count;
						ViewBag.OverallRating = Math.Round((decimal)overallRating, 2);
					}
				}

				// get Laptop by Id ..
				var laptop = await _db.Laptops
					.Include(l => l.Brand)
					.Include(l => l.Category)
					.Include(l => l.Seller)
					.FirstOrDefaultAsync(l => l.Id == id);

				// get laptop images ..
				var lapImages = await _db.LaptopImages.Where(i => i.LapId == id).ToListAsync();
				ViewBag.LaptopImages = lapImages;

				return View(laptop);
			}
			else { return NotFound(); }
		}


		// POST: /AddLaptopReview/100
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LaptopDetails(int? id, [Bind("CustomerName,CustomerEmail,Rate,Comment,LapId")] LaptopReviews review)
		{
			if (id != null) 
			{
				// Add review
				review.LapId = id;
				_db.LaptopReviews.Add(review);
				_db.SaveChanges();
				return RedirectToAction(nameof(LaptopDetails));
			}

			// get Laptop by Id ..
			var laptop = await _db.Laptops
				.Include(l => l.Brand)
				.Include(l => l.Category)
				.Include(l => l.Seller)
				.FirstOrDefaultAsync(l => l.Id == id);

			return View(laptop);
		}


		// GET: /AccessoryDetails/1004
		public async Task<IActionResult> AccessoryDetails(int? id)
		{
			if (id != null)
			{
				// get reviews for Product View
				var reviews = await _db.AccessoryReviews.Where(f => f.AccId == id).ToListAsync();
				if (reviews != null)
				{
					ViewBag.Reviews = reviews;
					// get the count of each Rate (1..5)
					ViewBag.FiveStarsCount  = await _db.AccessoryReviews.Where(f => f.AccId == id && f.Rate == 5).CountAsync();
					ViewBag.FourStarsCount  = await _db.AccessoryReviews.Where(f => f.AccId == id && f.Rate == 4).CountAsync();
					ViewBag.ThreeStarsCount = await _db.AccessoryReviews.Where(f => f.AccId == id && f.Rate == 3).CountAsync();
					ViewBag.TwoStarsCount	= await _db.AccessoryReviews.Where(f => f.AccId == id && f.Rate == 2).CountAsync();
					ViewBag.OneStarCount	= await _db.AccessoryReviews.Where(f => f.AccId == id && f.Rate == 1).CountAsync();
					// get overall Rating
					decimal? overallRating = 0;
					foreach (var review in reviews)
					{
						overallRating += review.Rate;
					}
					if (reviews.Count != 0)
					{
						overallRating /= reviews.Count;
						ViewBag.OverallRating = Math.Round((decimal)overallRating, 2);
					}
				}

				// get Laptop by Id ..
				var accessory = await _db.Accessories
					.Include(l => l.Brand)
					.Include(l => l.Category)
					.Include(l => l.Seller)
					.FirstOrDefaultAsync(l => l.Id == id);

				// get laptop images ..
				var accImages = await _db.AccessoryImages.Where(i => i.AccId == id).ToListAsync();
				ViewBag.AccessoryImages = accImages;

				return View(accessory);
			}
			else { return NotFound(); }
		}


		// POST: /AddLaptopReview/100
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AccessoryDetails(int? id, [Bind("CustomerName,CustomerEmail,Rate,Comment,LapId")] AccessoryReviews review)
		{
			if (id != null)
			{
				// Add review
				review.AccId = id;
				_db.AccessoryReviews.Add(review);
				_db.SaveChanges();
				return RedirectToAction(nameof(AccessoryDetails));
			}

			// get Laptop by Id ..
			var accessory = await _db.Accessories
				.Include(l => l.Brand)
				.Include(l => l.Category)
				.Include(l => l.Seller)
				.FirstOrDefaultAsync(l => l.Id == id);

			return View(accessory);
		}


		// GET: /Contact
		public IActionResult Contact() => View();


        // POST: /Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(CustomerMessage msg)
        {
            if (ModelState.IsValid) 
            {
                _db.CustomerMessages.Add(msg);
                await _db.SaveChangesAsync();
            }

            return View(msg);
        }


		
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}