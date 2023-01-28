using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;
using Lapshop.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lapshop.Controllers
{
    public class LaptopCatsController : Controller
    {
        private readonly LaptopCategoryRepo repo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LaptopCatsController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            repo = new LaptopCategoryRepo();
        }

        
        // GET: LaptopCats
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAllAsync());
        }


        // GET: LaptopCats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var lapCategory = await repo.GetOneById(id);
            if (lapCategory == null)
            {
                return NotFound();
            }

            return View(lapCategory);
        }


        // GET: LaptopCats/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: LaptopCats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LapCategory lapCategory)
        {
            if (ModelState.IsValid)
            {
                // upload image ..
                await UploadImage(lapCategory);
                // save to Database ..
                await repo.Create(lapCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(lapCategory);
        }


        // GET: LaptopCats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var lapCategory = await repo.GetOneById(id);
            if (lapCategory == null)
            {
                return NotFound();
            }
            return View(lapCategory);
        }


        // POST: LaptopCats/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null) return NotFound();

            // Remove old image from server
            await RemoveImageFromServer(id);

            var category = await repo.GetOneById(id);
            if (await TryUpdateModelAsync(category, "", s => s.Name, s => s.Description, s => s.Image, s => s.ImageFile))
            {
                try
                {
                    category.Image = category.ImageFile.FileName;
                    // save new Image ..
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


        // GET: LaptopCats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lapCategory = await repo.GetOneById(id);
            if (lapCategory == null)
            {
                return NotFound();
            }

            return View(lapCategory);
        }


        // POST: LaptopCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lapCategory = await repo.GetOneById(id);
            if (lapCategory != null)
            {
                // delete image from server
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/LaptopCategories/", lapCategory.Image);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                // remove from DB
                await repo.Delete(lapCategory);
            }

            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> LapCategoryExists(int id) => await repo.GetOneById(id) != null;


        private async Task UploadImage(LapCategory lapCategory) 
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(lapCategory.ImageFile.FileName);
            string extension = Path.GetExtension(lapCategory.ImageFile.FileName);
            // set image name ..
            lapCategory.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            // save image on server
            string path = Path.Combine(wwwRootPath + "/Images/Upload/LaptopCategories/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await lapCategory.ImageFile.CopyToAsync(fileStream);
            }
        }

        private async Task RemoveImageFromServer(int? id) 
        {
            // get category
            var category = await repo.GetOneById(id);
            // find old image 
            string oldPath  = Path.Combine(_hostEnvironment.WebRootPath + "/Images/Upload/LaptopCategories/", category.Image);
            if (System.IO.File.Exists(oldPath))
            {
                // update old image ..
                System.IO.File.Delete(oldPath);
            }
        }
    }
}
