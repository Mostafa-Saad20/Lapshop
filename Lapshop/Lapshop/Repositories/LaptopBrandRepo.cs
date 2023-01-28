using Lapshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    // Laptop Brand ..
    public class LaptopBrandRepo : ILapshopRepository<LapBrand>
    {
        private readonly LapshopDbContext db;

        public LaptopBrandRepo()
        {
            db = new LapshopDbContext();

        }

        public async Task Create(LapBrand brand)
        {
            db.LapBrands.Add(brand);
            await db.SaveChangesAsync();
        }

        public async Task Delete(LapBrand brand)
        {
            db.LapBrands.Remove(brand);
            await db.SaveChangesAsync();
        }

        public async Task<List<LapBrand>> GetAllAsync()
        {
            return await db.LapBrands.ToListAsync();
        }

        public async Task<LapBrand> GetOneById(int? id)
        {
            return await db.LapBrands.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Update(LapBrand brand)
        {
            db.Update(brand);
            await db.SaveChangesAsync();
        }
    }
}
