using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Configuration", Schema = "public")]
    public class Configuration : BaseEntity<int>
    {
    }
}
