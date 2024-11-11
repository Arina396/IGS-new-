using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class GamePageController : Controller
    {
        public IActionResult GamePage()
        {
            var gameDetails = _gameRepository.GetGameById(gameId);
            var comments = _commentRepository.GetCommentsByGameId(gameId); // Получение комментариев из базы

            var model = new IndexModel
            {
                GameDetails = gameDetails,
                Comments = comments
            };

            return View(model);
        }
    }
}
