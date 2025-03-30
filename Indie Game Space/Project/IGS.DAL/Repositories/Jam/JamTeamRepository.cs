using IGS.DAL.Interfaces;
using IGS.DAL.Interfaces.Jam;
using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGS.DAL.Repositories.Jam
{
    public class JamTeamRepository : IJamTeamRepository

    {
        private readonly ApplicationDbContext _dbContext;

        public JamTeamRepository(ApplicationDbContext db) => _dbContext = db;

        public async Task<bool> Create(JamTeam entity)
        {
            await _dbContext.JamTeam.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(JamTeam entity)
        {
            _dbContext.JamTeam.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<JamTeam> GetById(int id) => await _dbContext.JamTeam.FirstOrDefaultAsync(team => team.Id == id);

        public async Task<List<JamTeam>> Select() => await _dbContext.JamTeam.ToListAsync();
    }
}