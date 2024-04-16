using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SearchQueries
{
    public class UsersSearchQuery : BaseSearchQuery
    {
        public string? UserName { get; set; }
    }
}
