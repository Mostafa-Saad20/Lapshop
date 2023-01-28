using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using Lapshop.Repositories;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace Lapshop.Controllers
{
    public class LaptopsController : Controller
    {
        private readonly LaptopRepo repo;
        private IWebHostEnvironment _hostEnvironment;

        public LaptopsController(IWebHostEnvironment hostEnvironment)
        {
            repo = new LaptopRepo();
            _hostEnvironment = hostEnvironment;
        }

        // GET: Laptops
        public async Task<IActionResult> Index(int? pageNumber)
        {
            if (SellerId() != null)
            {
                // display paged list of Laptops
                int pageSize = 4;
                int sellerId = int.Parse(SellerId());
                var sellerLaptops = await PaginatedList<Laptop>.CreateAsync(repo.GetSellerLaptop(sellerId), pageNumber ?? 1, pageSize);
                return View(sellerLaptops);
            }
            else return Redirect("~/Sellers/Login");
        }

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var laptop = await repo.GetOneById(id);

                if (laptop == null) return NotFound();

                else {
                    // get laptop images from DB by Id
                    var laptopImages = await repo.db.LaptopImages.Where(l => l.LapId == id).ToListAsync();
                    ViewBag.LaptopImages = laptopImages;
                    return View(laptop);
                }
            }
            else return Redirect("~/Sellers/Login");
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            if (SellerId() != null)
            {
                ViewData["BrandId"] = new SelectList(repo.db.LapBrands, "Id", "Name");
                ViewData["CategoryId"] = new SelectList(repo.db.LapCategories, "Id", "Name");
                return View();
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Laptops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Laptop laptop, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                // set laptop seller Id ..
                if (SellerId() != null)
                {
                    laptop.SellerId = int.Parse(SellerId());
                    // save laptop in DB
                    await repo.Create(laptop);
                    
                    // upload images to server and DB
                    await UploadImages(Images, laptop.Id);

					// get first image 
					var laptopImage = await repo.db.LaptopImages
						.Where(a => a.LapId == laptop.Id).FirstOrDefaultAsync();
					// update accessory 
					if (laptopImage != null)
					{
						laptop.Image = laptopImage.ImageName;
						await repo.Update(laptop);
					}

					return RedirectToAction(nameof(Index));
                }
                else return Redirect("~/Sellers/Login");
            }
            ViewData["BrandId"] = new SelectList(repo.db.LapBrands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(repo.db.LapCategories, "Id", "Name");
            return View(laptop);
        }


        // GET: Laptops/Edit/5
        public async Task<IActionResult> EditDetails(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var laptop = await repo.GetOneById(id);

                if (laptop == null) return NotFound();

                ViewData["BrandId"] = new SelectList(repo.db.LapBrands, "Id", "Name", laptop.Brand.Name);
                ViewData["CategoryId"] = new SelectList(repo.db.LapCategories, "Id", "Name", laptop.Category.Name);
                return View(laptop);
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Laptops/Edit/5
        [HttpPost, ActionName("EditDetails")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLaptop(Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                if (SellerId() != null)
                {
                    // save Seller Id ..
                    laptop.SellerId = int.Parse(SellerId());
                    // update laptop data in DB
                    await repo.Update(laptop);
                    return Redirect("~/Laptops/Details/" + laptop.Id);
                }
                else return Redirect("~/Sellers/Login");
            }
            ViewData["BrandId"] = new SelectList(repo.db.LapBrands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(repo.db.LapCategories, "Id", "Name");
            return View(laptop);
        }

        // GET: Laptops/DeleteLaptop/5
        public async Task<IActionResult> DeleteLaptop(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var laptop = await repo.GetOneById(id);

                if (laptop == null) return NotFound();

                return View(laptop);
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("DeleteLaptop")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await repo.GetOneById(id);
            // remove laptop..
            if (laptop != null) await repo.Delete(laptop);

            return RedirectToAction(nameof(Index));
        }


        // ------------------------------------ //
        // *********** Laptop Images ********** //
        // GET: /EditImages
        public async Task<IActionResult> EditImages(int id)
        {
            if (SellerId() != null)
            {
                // get laptop images from DB by Id
                var laptopImages = await repo.db.LaptopImages.Where(l => l.LapId == id).ToListAsync();
                return View(laptopImages);
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: /EditImages
        [HttpPost, ActionName("EditImages")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(int id, List<IFormFile> images)
        {
            if (images != null)
            {
                // set laptop seller Id ..
                if (SellerId() != null)
                {
                    // upload images to server and DB
                    await UploadImages(images, id);

					// get first image 
					var laptopImage = await repo.db.LaptopImages
						.Where(a => a.LapId == id).FirstOrDefaultAsync();
					// update laptop image 
					if (laptopImage != null)
					{
                        var laptop = await repo.GetOneById(id);
						laptop.Image = laptopImage.ImageName;
						await repo.Update(laptop);
					}

					return Redirect("~/Laptops/Details/" + id);
                }
                else return Redirect("~/Sellers/Login");
            }
            else { ViewBag.ErrorImage = "Please Upload file"; }
            return View(images);
        }

        // GET: Laptops/DeleteImage/5
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var laptopImage = await repo.db.LaptopImages.FindAsync(id);

                if (laptopImage == null) return NotFound();

                return View(laptopImage);
            }
            else return Redirect("~/Sellers/Login");
        }


        // POST: Laptops/DeleteImage/5
        [HttpPost, ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImageConfirmed(int id)
        {
            // find Image 
            var laptopImage = await repo.db.LaptopImages.FindAsync(id);
            int lapId = laptopImage.LapId;
            // remove laptopImage ..
            if (laptopImage != null)
            {
                // delete image from server
                string? imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/LaptopImages", laptopImage.LapId.ToString(), laptopImage.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                // remove from DB
                repo.db.Remove(laptopImage);
                await repo.db.SaveChangesAsync();
                return Redirect("~/Laptops/Details/" + lapId);
            }
            else { return NotFound(); }
        }


        // --------------------------------------------- //
        // ******* Image Upload ****** //
        public async Task UploadImages(List<IFormFile> postedImages, int laptopId)
        {
            // make directory for images
            string wwwPath = _hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwPath, "Images/Upload/LaptopImages", laptopId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            // Add multiple images to Folder and list
            foreach (IFormFile image in postedImages)
            {
                // set image name & path
                string imageName = DateTime.Now.ToString("fffff") + Path.GetFileName(image.FileName);
                string imagePath = Path.Combine(path, imageName);
                using (FileStream stream = new FileStream(imagePath, FileMode.Create)) 
                {
                    // upload to server
                    image.CopyTo(stream);
                    // upload to DB
                    LaptopImages laptopImage = new()
                    {
                        ImageName = imageName,
                        LapId = laptopId
                    };
                    await repo.db.LaptopImages.AddAsync(laptopImage);
                    await repo.db.SaveChangesAsync();
                }
            }
        }


        private string SellerId() => HttpContext.Session.GetString("SellerId");
    
    }
}
