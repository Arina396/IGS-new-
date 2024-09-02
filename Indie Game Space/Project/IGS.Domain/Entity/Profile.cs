using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace IGS.Domain.Entity
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        public string? ImageName { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Country { get; set; }

        public string? URL { get; set; }

        public string? GitHubLink { get; set; }
    }
}
