using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using Lapshop.Repositories;

namespace Lapshop.Controllers
{
    public class AccessoryCatsController : Controller
    {
        private readonly AccessoryCategoryRepo repo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccessoryCatsController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            repo = new AccessoryCategoryRepo();
        }

        // GET: AccessoryCats
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAllAsync());
        }

        // GET: AccessoryCats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accCategory = await repo.GetOneById(id);
            if (accCategory == null)
            {
                return NotFound();
            }

            return View(accCategory);
        }

        // GET: AccessoryCats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessoryCats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccCategory accCategory)
        {
            if (ModelState.IsValid)
            {
                // upload image ..
                await UploadImage(accCategory);
                // save in DB
                await repo.Create(accCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(accCategory);
        }

        // GET: AccessoryCats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accCategory = await repo.GetOneById(id);
            if (accCategory == null)
            {
                return NotFound();
            }
            return View(accCategory);
        }

        // POST: AccessoryCats/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null) return NotFound();

            // remove image of old category ..
            await RemoveImageFromServer(id);
            // get posted category by ID ..
            var category = await repo.GetOneById(id);
            if (await TryUpdateModelAsync(category, "", c => c.Name, c => c.Description, c => c.Image, c => c.ImageFile))
            {
                try
                {
                    category.Image = category.ImageFile.FileName;
                    // upload new Image ..
                    await UploadImage(category);
                    // save changes in DB
                    await repo.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */) 
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }

            }

            return View(category);
        }

        // GET: AccessoryCats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accCategory = await repo.GetOneById(id);
            if (accCategory == null)
            {
                return NotFound();
            }

            return View(accCategory);
        }

        // POST: AccessoryCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accCategory = await repo.GetOneById(id);
            if (accCategory != null)
            {
                // delete image from server
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/AccessoryCategories/", accCategory.Image);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                // remove from DB
                await repo.Delete(accCategory);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AccCategoryExists(int id) => await repo.GetOneById(id) != null;

        private async Task UploadImage(AccCategory accCategory)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName  = Path.GetFileNameWithoutExtension(accCategory.ImageFile.FileName);
            string extension = Path.GetExtension(accCategory.ImageFile.FileName);
            // set image name ..
            accCategory.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            // save image on server
            string path = Path.Combine(wwwRootPath + "/Images/Upload/AccessoryCategories/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await accCategory.ImageFile.CopyToAsync(fileStream);
            }
        }

        private async Task RemoveImageFromServer(int? id)
        {
            // get category
            var category = await repo.GetOneById(id);
            // find old image 
            string oldPath = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/AccessoryCategories/", category.Image);
            if (System.IO.File.Exists(oldPath))
            {
                // update old image ..
                System.IO.File.Delete(oldPath);
            }
        }
    }
}
