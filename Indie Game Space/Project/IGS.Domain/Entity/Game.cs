namespace IGS.Domain.Entity
{
    public class Game
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public string? Creator { get; set; }
    }
}
