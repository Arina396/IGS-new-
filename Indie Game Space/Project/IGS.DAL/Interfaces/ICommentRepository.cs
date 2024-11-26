using IGS.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGS.DAL.Interfaces
{
	public interface ICommentRepository : IBaseRepository<Comments>
	{
		Task<List<Comments>> GetByGameIdWithUsers(int gameId);
        // Метод для получения комментария по его ID
        Task<Comments> GetById(int commentId);

		// Метод для получения всех комментариев по ID игры
		Task<List<Comments>> GetByGameId(int gameId);

		// Метод для получения всех комментариев пользователя по его ID
		Task<List<Comments>> GetByUserId(int userId);

		// Метод для обновления текста комментария
		Task<bool> UpdateCommentText(int commentId, string newText);

		// Метод для сохранения нового комментария (можно использовать метод из IBaseRepository)
		Task<bool> Create(Comments comment);
	}
}
