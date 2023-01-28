using Lapshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Lapshop.Repositories
{
    public class AccessoryCategoryRepo : ILapshopRepository<AccCategory>
    {
        private readonly LapshopDbContext db;

        public AccessoryCategoryRepo()
        {
            db = new LapshopDbContext();
        }

        public async Task Create(AccCategory cat)
        {
            db.AccCategories.Add(cat);
            await db.SaveChangesAsync();
        }

        public async Task Delete(AccCategory cat)
        {
            db.AccCategories.Remove(cat);
            await db.SaveChangesAsync();
        }

        public async Task<List<AccCategory>> GetAllAsync()
        {
            return await db.AccCategories.ToListAsync();
        }

        public async Task<AccCategory> GetOneById(int? id)
        {
            return await db.AccCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(AccCategory cat)
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
