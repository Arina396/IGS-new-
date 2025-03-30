using System.ComponentModel.DataAnnotations;

namespace IGS.Domain.ViewModels.Jam
{
    public class JamRegistrationModel
    {
        [Required(ErrorMessage = "Название команды обязательно.")]
        public string NameOfTeam { get; set; }

        [Required(ErrorMessage = "Ник капитана обязателен.")]
        public string NameOfCapitan { get; set; }

        public string MembersOfTeam { get; set; } // Ники участников через запятую, необязательное поле

    }
}