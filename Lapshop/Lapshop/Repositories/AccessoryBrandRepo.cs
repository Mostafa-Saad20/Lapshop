using Lapshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    public class AccessoryBrandRepo : ILapshopRepository<AccBrand>
    {
        private readonly LapshopDbContext db;

        public AccessoryBrandRepo() 
        {
            db = new LapshopDbContext();
        }

        public async Task Create(AccBrand brand)
        {
            db.AccBrands.Add(brand);
            await db.SaveChangesAsync();
        }

        public async Task Delete(AccBrand brand)
        {
            db.AccBrands.Remove(brand);
            await db.SaveChangesAsync();
        }

        public async Task<List<AccBrand>> GetAllAsync()
        {
            return await db.AccBrands.ToListAsync();
        }

        public async Task<AccBrand> GetOneById(int? id)
        {
            return await db.AccBrands.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Update(AccBrand brand)
        {
            db.AccBrands.Update(brand);
            await db.SaveChangesAsync();
        }
    }
}
