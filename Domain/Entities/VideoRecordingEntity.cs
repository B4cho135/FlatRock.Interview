using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("VideoRecordings", Schema = "public")]
    public class VideoRecordingEntity : BaseEntity<int>
    {
        [Column(TypeName = "bytea")]
        public byte[] VideoRecording { get; set; } = [];
        public string? SessionId { get; set; }
    }
}
