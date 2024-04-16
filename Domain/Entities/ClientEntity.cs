using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Clients", Schema = "public")]
    public class ClientEntity : BaseEntity<int>
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
