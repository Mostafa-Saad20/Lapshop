using Lapshop.Models;
using Lapshop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Lapshop.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly AccessoriesRepo repo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccessoriesController(IWebHostEnvironment hostEnvironment)
        {
            repo = new AccessoriesRepo();
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
                var sellerLaptops = await PaginatedList<Accessory>.CreateAsync(repo.GetSellerAccessory(sellerId), pageNumber ?? 1, pageSize);
                return View(sellerLaptops);
            }
            else return Redirect("~/Sellers/Login");
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var accessory = await repo.GetOneById(id);

                if (accessory == null) return NotFound();

                else
                {
                    // get laptop images from DB by Id
                    var accessoryImages = await repo.db.AccessoryImages.Where(l => l.AccId == id).ToListAsync();
                    ViewBag.AccImages = accessoryImages;
                    return View(accessory);
                }
            }
            else return Redirect("~/Sellers/Login");
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            if (SellerId() != null)
            {
                ViewData["BrandId"] = new SelectList(repo.db.AccBrands, "Id", "Name");
                ViewData["CategoryId"] = new SelectList(repo.db.AccCategories, "Id", "Name");
                return View();
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Accessories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Accessory accessory, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                // set laptop seller Id ..
                if (SellerId() != null)
                {
                    accessory.SellerId = int.Parse(SellerId());
                    // save accessory in DB
                    await repo.Create(accessory);

                    // upload images to server and DB
                    await UploadImages(Images, accessory.Id);

                    // get first image 
                    var accessoryImage = await repo.db.AccessoryImages
                        .Where(a => a.AccId == accessory.Id).FirstOrDefaultAsync();
                    // update accessory 
                    if (accessoryImage != null)
                    {
                        accessory.Image = accessoryImage.ImageName;
                        await repo.Update(accessory);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else return Redirect("~/Sellers/Login");
            }
            ViewData["BrandId"] = new SelectList(repo.db.AccBrands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(repo.db.AccCategories, "Id", "Name");
            return View(accessory);
        }


        // GET: Accessories/Edit/5
        public async Task<IActionResult> EditDetails(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var accessory = await repo.GetOneById(id);

                if (accessory == null) return NotFound();

                ViewData["BrandId"] = new SelectList(repo.db.AccBrands, "Id", "Name", accessory.Brand.Name);
                ViewData["CategoryId"] = new SelectList(repo.db.AccCategories, "Id", "Name", accessory.Category.Name);
                return View(accessory);
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Accessories/Edit/5
        [HttpPost, ActionName("EditDetails")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccessory(Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                if (SellerId() != null)
                {
                    // save Seller Id ..
                    accessory.SellerId = int.Parse(SellerId());
                    // update laptop data in DB
                    await repo.Update(accessory);
                    return Redirect("~/Accessories/Details/" + accessory.Id);
                }
                else return Redirect("~/Sellers/Login");
            }
            ViewData["BrandId"] = new SelectList(repo.db.AccBrands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(repo.db.AccCategories, "Id", "Name");
            return View(accessory);
        }

        // GET: Accessories/DeleteAccessory/5
        public async Task<IActionResult> DeleteAccessory(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var accessory = await repo.GetOneById(id);

                if (accessory == null) return NotFound();

                return View(accessory);
            }
            else return Redirect("~/Sellers/Login");
        }

        // POST: Accessories/DeleteAccessory/5
        [HttpPost, ActionName("DeleteAccessory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessory = await repo.GetOneById(id);
            // remove accessory ..
            if (accessory != null) await repo.Delete(accessory);

            return RedirectToAction(nameof(Index));
        }


        // ------------------------------------ //
        // *********** Accessory Images ********** //
        // GET: /EditImages
        public async Task<IActionResult> EditImages(int id)
        {
            if (SellerId() != null)
            {
                // get laptop images from DB by Id
                var accessoryImages = await repo.db.AccessoryImages.Where(l => l.AccId == id).ToListAsync();
                return View(accessoryImages);
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
					var accessoryImage = await repo.db.AccessoryImages
						.Where(a => a.AccId == id).FirstOrDefaultAsync();
					// update accessory image 
					if (accessoryImage != null)
					{
						var accessory = await repo.GetOneById(id);
						accessory.Image = accessoryImage.ImageName;
						await repo.Update(accessory);
					}

					return Redirect("~/Accessories/Details/" + id);
                }
                else return Redirect("~/Sellers/Login");
            }
            else { ViewBag.ErrorImage = "Please Upload file"; }
            return View(images);
        }

        // GET: /DeleteImage/5
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (SellerId() != null)
            {
                if (id == null) return NotFound();

                var accessoryImage = await repo.db.AccessoryImages.FindAsync(id);

                if (accessoryImage == null) return NotFound();

                return View(accessoryImage);
            }
            else return Redirect("~/Sellers/Login");
        }


        // POST: /DeleteImage/5
        [HttpPost, ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImageConfirmed(int id)
        {
            // find Image 
            var accessoryImage = await repo.db.AccessoryImages.FindAsync(id);
            int accId = accessoryImage.AccId;
            // remove accessory Image ..
            if (accessoryImage != null)
            {
                // delete image from server
                string? imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/AccessoryImages", accessoryImage.AccId.ToString(), accessoryImage.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                // remove from Accessory images in DB
                repo.db.Remove(accessoryImage);
                await repo.db.SaveChangesAsync();
                return Redirect("~/Accessories/Details/" + accId);
            }
            else { return NotFound(); }
        }


        // --------------------------------------------- //
        // ******* Image Upload ****** //
        public async Task UploadImages(List<IFormFile> postedImages, int accessoryId)
        {
            // make directory for images
            string wwwPath = _hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwPath, "Images/Upload/AccessoryImages", accessoryId.ToString());
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
                using FileStream stream = new(imagePath, FileMode.Create);
                // upload to server
                image.CopyTo(stream);
                // upload to DB
                AccessoryImages accessoryImage = new()
                {
                    ImageName = imageName,
                    AccId = accessoryId
                };
                await repo.db.AccessoryImages.AddAsync(accessoryImage);
                await repo.db.SaveChangesAsync();
            }
        }

        private string SellerId() => HttpContext.Session.GetString("SellerId");

    }

}
