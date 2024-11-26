using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGS.Domain.Entity
{
    public class Comments
    {
        [Key]
        public int Comment_id { get; set; } // Первичный ключ

        public int Game_id { get; set; }

        [ForeignKey(nameof(User))] // Связь через поле Id таблицы Profile
        public int User_Id { get; set; } // Ссылка на Id из Profile

        public string? Comment { get; set; }

        public Profile User { get; set; } // Навигационное свойство
    }
}
