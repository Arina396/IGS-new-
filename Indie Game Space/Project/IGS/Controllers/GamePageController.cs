using IGS.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using IGS.DAL.Interfaces;
using IGS.Domain.ViewModels.Game;


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
                    Name = gameDetails.Name,
                    ImageName = gameDetails.ImageName,
                    Price = gameDetails.Price,
                    Creator = gameDetails.Creator,
                    Description = gameDetails.Description
                };

                var comments = await _commentRepository.GetByGameId(id);
                var commentViewModels = comments.Select(comment => new CommentViewModel
                {
                    UserId = comment.User_Id,
                    CommentText = comment.Comment
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
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while loading the game page.");
            }
        }
    }
}
