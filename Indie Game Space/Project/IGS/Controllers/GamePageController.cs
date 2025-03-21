using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using IGS.Domain.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class GamePageController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public GamePageController(IGameRepository gameRepository,
                                ICommentRepository commentRepository,
                                IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
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
                    ScrinshotName = gameDetails.ScrinshotName,
                    ScrinshotName2 = gameDetails.ScrinshotName2,
                    ScrinshotName3 = gameDetails.ScrinshotName3,
                    Price = 1, // Предполагаю, что Price остался в интерфейсе репозитория
                    Creator = gameDetails.Creator,
                    Description = gameDetails.Description, // Краткое описание
                    Genre = gameDetails.Genre,
                    Link = gameDetails.Link
                };

                // Передаем полное описание через ViewData
                ViewData["LargeDescription"] = gameDetails.LargeDescription;

                var comments = await _commentRepository.GetByGameIdWithUsers(id);
                var commentViewModels = comments.Select(comment => new CommentViewModel
                {
                    UserName = comment.User?.Name ?? "Anonymous",
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
                TempData["Error"] = $"Произошла ошибка: {ex.Message}";
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int gameId, string commentText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commentText))
                {
                    TempData["Error"] = "Комментарий не может быть пустым";
                    return RedirectToAction("GamePage", new { id = gameId });
                }

                var userLogin = User.Identity.Name;
                var user = await _userRepository.GetByLogin(userLogin);

                if (user == null)
                {
                    return Unauthorized();
                }

                var newComment = new Comments
                {
                    Game_id = gameId,
                    User_Id = user.Id,
                    Comment = commentText
                };

                var isAdded = await _commentRepository.Create(newComment);
                if (!isAdded)
                {
                    TempData["Error"] = "Не удалось добавить комментарий";
                }

                return RedirectToAction("GamePage", new { id = gameId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Произошла ошибка: {ex.Message}";
                return RedirectToAction("GamePage", new { id = gameId });
            }
        }
    }
}