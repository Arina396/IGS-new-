using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGS.Domain.Entity
{
    [Table("Team")]
    public class JamTeam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int JamId { get; set; } // вернуть когда будет JamId 
    }
}
