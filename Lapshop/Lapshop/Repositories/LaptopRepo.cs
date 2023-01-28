using Lapshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    public class LaptopRepo: ILapshopRepository<Laptop>
    {
        public LapshopDbContext db;
        
        public LaptopRepo()
        {
            db = new LapshopDbContext();
        }

        public async Task Create(Laptop laptop)
        {
            db.Laptops.Add(laptop);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Laptop laptop)
        {
            db.Remove(laptop);
            await db.SaveChangesAsync();
        }

        public async Task<List<Laptop>> GetAllAsync()
        {
            return await db.Laptops
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Seller)
                .ToListAsync();
        }

        public IQueryable<Laptop> GetSellerLaptop(int id) 
        {
            var laptop = from l in db.Laptops where l.SellerId == id select l;
            return laptop
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Seller).AsNoTracking();
        }

        public async Task<Laptop> GetOneById(int? id)
        {
            return await db.Laptops
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Seller)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task Update(Laptop laptop)
        {
            db.Update(laptop);
            await db.SaveChangesAsync();
        }

    }
}
