using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IGS.Domain.Entity;

namespace IGS.Views.GamePage
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public Games2 GameDetails { get; set; }
        public IEnumerable<Comments> Comments { get; set; } // Список комментариев
    }
}
