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
    public class AccessoryBrandsController : Controller
    {
        private readonly AccessoryBrandRepo repo;

        public AccessoryBrandsController()
        {
            repo = new AccessoryBrandRepo();
        }

        // GET: AccessoryBrands
        public async Task<IActionResult> Index()
        {
              return View(await repo.GetAllAsync());
        }

        // GET: AccessoryBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accBrand = await repo.GetOneById(id);
            if (accBrand == null)
            {
                return NotFound();
            }

            return View(accBrand);
        }

        // GET: AccessoryBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessoryBrands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Nationality")] AccBrand accBrand)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(accBrand);
                return RedirectToAction(nameof(Index));
            }
            return View(accBrand);
        }

        // GET: AccessoryBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accBrand = await repo.GetOneById(id);
            if (accBrand == null)
            {
                return NotFound();
            }
            return View(accBrand);
        }

        // POST: AccessoryBrands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Nationality")] AccBrand accBrand)
        {
            if (id != accBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repo.Update(accBrand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AccBrandExists(accBrand.Id))
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
            return View(accBrand);
        }

        // GET: AccessoryBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accBrand = await repo.GetOneById(id);
            if (accBrand == null)
            {
                return NotFound();
            }

            return View(accBrand);
        }

        // POST: AccessoryBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accBrand = await repo.GetOneById(id);
            if (accBrand != null)
            {
                await repo.Delete(accBrand);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AccBrandExists(int id) => await repo.GetOneById(id) != null;
    }
}
