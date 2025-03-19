using System.ComponentModel.DataAnnotations;

namespace IGS.Domain.Entity
{
    public class Games2
    {
        [Key]
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public string Name { get; set; }
        public string? Genre { get; set; }
        public string? ScrinshotName { get; set; }
        public string? Description { get; set; }
        public string? Creator { get; set; }
        public int? User_Id { get; set; }
        public string? Link { get; set; }
    }
}