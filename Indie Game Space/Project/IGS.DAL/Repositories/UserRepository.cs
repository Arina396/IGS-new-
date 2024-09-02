using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IGS.DAL.Repositories
{
	public class UserRepository : IUserRepository
	{
		private ApplicationDbContext _dbContext;

		public UserRepository(ApplicationDbContext db) => _dbContext = db;

		public async Task<bool> Create(User entity)
		{
			await _dbContext.User.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> Delete(User entity)
		{
			_dbContext.User.Remove(entity);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<User> GetById(int id) => await _dbContext.User.FirstOrDefaultAsync(user => user.Id == id);

		public async Task<User> GetByEmail(string email) => await _dbContext.User.FirstOrDefaultAsync(user => user.Email == email);

		public async Task<User> GetByLogin(string login) => await _dbContext.User.FirstOrDefaultAsync(user => user.Login == login);

		public async Task<List<User>> Select() => await _dbContext.User.ToListAsync();
	}
}
