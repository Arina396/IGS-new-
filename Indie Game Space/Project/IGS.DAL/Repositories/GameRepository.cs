using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IGS.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private ApplicationDbContext _dbContext;

        public GameRepository(ApplicationDbContext db) => _dbContext = db;

        public async Task<bool> Create(Games2 entity)
        {
            await _dbContext.Games2.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Games2 entity)
        {
            _dbContext.Games2.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Games2> GetById(int id) => await _dbContext.Games2.FirstOrDefaultAsync(game => game.Id == id);

        public async Task<List<Games2>> Select() => await _dbContext.Games2.ToListAsync();
    }
}
