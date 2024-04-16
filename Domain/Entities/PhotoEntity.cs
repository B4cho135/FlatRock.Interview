using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Photos", Schema = "public")]
    public class PhotoEntity : BaseEntity<int>
    {
        public byte[] Photo { get; set; } = [];
        public string? SessionId { get; set; }
    }
}
