using Microsoft.AspNetCore.Http;

namespace IGS.Domain.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}