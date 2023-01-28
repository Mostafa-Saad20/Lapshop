using Lapshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    public class AccessoriesRepo: ILapshopRepository<Accessory>
    {
        public LapshopDbContext db;
        
        public AccessoriesRepo()
        {
            db = new LapshopDbContext();
        }

        public async Task Create(Accessory accessory)
        {
            db.Accessories.Add(accessory);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Accessory accessory)
        {
            db.Remove(accessory);
            await db.SaveChangesAsync();
        }

        public async Task<List<Accessory>> GetAllAsync()
        {
            return await db.Accessories
                .Include(a => a.Brand)
                .Include(a => a.Category)
                .Include(a => a.Seller)
                .ToListAsync();
        }

        public IQueryable<Accessory> GetSellerAccessory(int id)
        {
            var accessory = from a in db.Accessories where a.SellerId == id select a;
            return accessory
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Seller).AsNoTracking();
        }

        public async Task<Accessory> GetOneById(int? id)
        {
            return await db.Accessories
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Seller)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task Update(Accessory accessory)
        {
            db.Update(accessory);
            await db.SaveChangesAsync();
        }
    }
}
