namespace IGS.Domain.ViewModels.Game
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageName { get; set; }

        public string? ScrinshotName { get; set; }
        public string? ScrinshotName2 { get; set; }
        public string? ScrinshotName3 { get; set; }

        public int Price { get; set; }

        public string? Genre { get; set; }

        public string Creator { get; set; }

        public string Description { get; set; }

        public string? Link { get; set; }
    }
}
