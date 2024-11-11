using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IGS.Views.GamePage
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public Game GameDetails { get; set; }
        public IEnumerable<Comment> Comments { get; set; } // Список комментариев
    }
}
