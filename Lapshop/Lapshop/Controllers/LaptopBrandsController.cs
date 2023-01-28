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
    public class LaptopBrandsController : Controller
    {
        private readonly LaptopBrandRepo repo;

        public LaptopBrandsController()
        {
            repo = new LaptopBrandRepo();
        }

        // GET: LaptopBrands
        public async Task<IActionResult> Index()
        {
            return View(await repo.GetAllAsync());
        }

        // GET: LaptopBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ) return NotFound();

            var lapBrand = await repo.GetOneById(id);
            if (lapBrand == null)
            {
                return NotFound();
            }

            return View(lapBrand);
        }

        // GET: LaptopBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LaptopBrands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Nationality")] LapBrand lapBrand)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(lapBrand);
                return RedirectToAction(nameof(Index));
            }
            return View(lapBrand);
        }

        // GET: LaptopBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapBrand = await repo.GetOneById(id);
            if (lapBrand == null)
            {
                return NotFound();
            }
            return View(lapBrand);
        }

        // POST: LaptopBrands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality")] LapBrand lapBrand)
        {
            if (id != lapBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repo.Update(lapBrand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LapBrandExists(lapBrand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lapBrand);
        }

        // GET: LaptopBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapBrand = await repo.GetOneById(id);
            if (lapBrand == null)
            {
                return NotFound();
            }

            return View(lapBrand);
        }

        // POST: LaptopBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lapBrand = await repo.GetOneById(id);
            if (lapBrand != null)
            {
                await repo.Delete(lapBrand);
            }
            
            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> LapBrandExists(int id) => await repo.GetOneById(id) != null;
        
    }
}
