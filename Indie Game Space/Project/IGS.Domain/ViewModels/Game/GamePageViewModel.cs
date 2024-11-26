using System.Collections.Generic;

namespace IGS.Domain.ViewModels.Game
{
    public class GamePageViewModel
    {
        public GameViewModel GameDetails { get; set; }
        public List<CommentViewModel> Comments { get; set; }
       
    }
}
