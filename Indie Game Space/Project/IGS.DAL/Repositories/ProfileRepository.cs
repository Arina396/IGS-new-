using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IGS.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext db) => _dbContext = db;

        public async Task<bool> Create(Profile entity)
        {
            await _dbContext.Profiles.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Profile entity)
        {
            _dbContext.Profiles.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Profile> GetById(int id) => await _dbContext.Profiles.FirstOrDefaultAsync(profile => profile.Id == id);

        public async Task<Profile> GetByName(string name) => await _dbContext.Profiles.FirstOrDefaultAsync(profile => profile.Name == name);

        public async Task<bool> SaveProfile(Profile profile)
        {
            _dbContext.Entry(profile).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<Profile>> Select() => await _dbContext.Profiles.ToListAsync();
    }
}
