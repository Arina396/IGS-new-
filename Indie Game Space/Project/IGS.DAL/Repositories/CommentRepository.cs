using System.Collections.Generic;
using System.Threading.Tasks;

public class CommentRepository : ICommentRepository
{
	private ApplicationDbContext _dbContext;

	public CommentRepository(ApplicationDbContext db) => _dbContext = db;

	// Метод для создания нового комментария
	public async Task<bool> Create(Comment entity)
	{
		await _dbContext.Comments.AddAsync(entity);
		await _dbContext.SaveChangesAsync();
		return true;
	}

	// Метод для удаления комментария
	public async Task<bool> Delete(Comment entity)
	{
		_dbContext.Comments.Remove(entity);
		await _dbContext.SaveChangesAsync();
		return true;
	}

	// Метод для получения комментария по его ID
	public async Task<Comment> GetById(int commentId)
	{
		return await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.Comment_id == commentId);
	}

	// Метод для получения всех комментариев по ID игры
	public async Task<List<Comment>> GetByGameId(int gameId)
	{
		return await _dbContext.Comments.Where(comment => comment.Game_id == gameId).ToListAsync();
	}

	// Метод для получения всех комментариев пользователя по его ID
	public async Task<List<Comment>> GetByUserId(int userId)
	{
		return await _dbContext.Comments.Where(comment => comment.User_Id == userId).ToListAsync();
	}

	// Метод для обновления текста комментария
	public async Task<bool> UpdateCommentText(int commentId, string newText)
	{
		var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Comment_id == commentId);
		if (comment == null)
		{
			return false; // Возвращает false, если комментарий не найден
		}

		comment.Comment = newText;
		_dbContext.Entry(comment).State = EntityState.Modified;
		await _dbContext.SaveChangesAsync();
		return true;
	}

	// Метод для получения всех комментариев
	public async Task<List<Comment>> Select()
	{
		return await _dbContext.Comments.ToListAsync();
	}
}