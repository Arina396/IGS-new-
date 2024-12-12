using IGS.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using IGS.DAL.Interfaces;
using IGS.Domain.ViewModels.Game;
using IGS.Domain.Entity;

namespace IGS.Controllers
{
    public class GamePageController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICommentRepository _commentRepository;

        public GamePageController(IGameRepository gameRepository, ICommentRepository commentRepository)
        {
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> GamePage(int id)
        {
            try
            {
                var gameDetails = await _gameRepository.GetById(id);
                if (gameDetails == null)
                {
                    return NotFound();
                }

                var gameViewModel = new GameViewModel
                {
                    Id = id,
                    Name = gameDetails.Name,
                    ImageName = gameDetails.ImageName,
                    Price = gameDetails.Price,
                    Creator = gameDetails.Creator,
                    Description = gameDetails.Description
                };

                // Получаем комментарии вместе с профилями пользователей
                var comments = await _commentRepository.GetByGameIdWithUsers(id);

                var commentViewModels = comments.Select(comment => new CommentViewModel
                {
                    UserName = comment.User?.Name ?? "Unknown", // Используем Name из Profile
                    Comment = comment.Comment
                }).ToList();

                var model = new GamePageViewModel
                {
                    GameDetails = gameViewModel,
                    Comments = commentViewModels
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GamePage: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
                // Log the exception if necessary

            }
        }


        // Новый метод для добавления комментария
        [HttpPost]
        public async Task<IActionResult> AddComment(int gameId, string commentText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commentText))
                {
                    return BadRequest("Комментарий не может быть пустым.");
                }

                // Получение текущего пользователя (пример с аутентификацией)
                var userId = 1; // тест добавить метод для ввода id текущего User

                var newComment = new Comments
                {
                    Game_id = gameId,
                    User_Id = userId,
                    Comment = commentText
                };

                var isAdded = await _commentRepository.Create(newComment);

                if (!isAdded)
                {
                    return StatusCode(500, "Не удалось добавить комментарий.");
                }

                return RedirectToAction("GamePage", new { id = gameId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddComment: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}