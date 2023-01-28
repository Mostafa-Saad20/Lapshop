using Lapshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    public class LaptopCategoryRepo: ILapshopRepository<LapCategory>
    {
        private readonly LapshopDbContext db;

        public LaptopCategoryRepo()
        {
            db = new LapshopDbContext();
        }

        public async Task Create(LapCategory cat)
        {
            db.LapCategories.Add(cat);
            await db.SaveChangesAsync();
        }

        public async Task Delete(LapCategory cat)
        {
            db.LapCategories.Remove(cat);
            await db.SaveChangesAsync();
        }

        public async Task<List<LapCategory>> GetAllAsync()
        {
            return await db.LapCategories.ToListAsync();
        }

        public async Task<LapCategory> GetOneById(int? id)
        {
            return await db.LapCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(LapCategory cat)
        {
            db.Update(cat);
            await db.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }

    }
}
