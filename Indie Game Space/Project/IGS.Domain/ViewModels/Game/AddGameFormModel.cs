using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace IGS.Domain.ViewModels.Game
{
    public class AddGameFormModel
    {
        [Required(ErrorMessage = "Название игры обязательно.")]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public IFormFile Cover { get; set; }

        public IFormFile Screenshot { get; set; }
        public IFormFile Screenshot2 { get; set; }
        public IFormFile Screenshot3 { get; set; }

        public string AdditionalDescription { get; set; }

        public string Link { get; set; }

        public string Creator { get; set; }

        public string Genre { get; set; }
    }
}