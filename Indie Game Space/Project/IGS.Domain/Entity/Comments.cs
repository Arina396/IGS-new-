using System.ComponentModel.DataAnnotations;

namespace IGS.Domain.Entity
{
    public class Comments
    {
        [Key]
        public int Comment_id { get; set; } // Предположительно, это ваш первичный ключ

        public int Game_id { get; set; }
        public int User_Id { get; set; }
        public string? Comment { get; set; }
    }
}
