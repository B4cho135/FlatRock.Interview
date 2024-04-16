using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ClientRecordings", Schema = "public")]
    public class ClientRecordingEntity : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public ClientEntity? Client { get; set; }
        public int VideoRecordingId { get; set; }
        public VideoRecordingEntity? VideoRecording { get; set; }
    }
}
