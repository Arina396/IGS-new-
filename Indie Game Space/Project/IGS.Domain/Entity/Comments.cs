namespace IGS.Domain.Entity
{
    public class Comments
    {
        public int Comment_id { get; set; }

        public int Game_id { get; set; }

        public int User_Id { get; set; }

        public string? Comment { get; set; }
        
    }
}
