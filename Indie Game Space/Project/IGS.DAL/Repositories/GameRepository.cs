using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IGS.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private ApplicationDbContext _dbContext;

        public GameRepository(ApplicationDbContext db) => _dbContext = db;

        public async Task<bool> Create(Game entity)
        {
            await _dbContext.Games.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Game entity)
        {
            _dbContext.Games.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Game> GetById(int id) => await _dbContext.Games.FirstOrDefaultAsync(game => game.Id == id);

        public async Task<List<Game>> Select() => await _dbContext.Games.ToListAsync();
    }
}
