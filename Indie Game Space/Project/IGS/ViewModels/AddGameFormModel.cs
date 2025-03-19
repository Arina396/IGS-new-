using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace IGS.ViewModels
{
    public class AddGameFormModel
    {
        [Required(ErrorMessage = "Название игры обязательно.")]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public IFormFile Cover { get; set; }

        public IFormFile Screenshot { get; set; }

        public string AdditionalDescription { get; set; }

        public string Link { get; set; }
    }
}