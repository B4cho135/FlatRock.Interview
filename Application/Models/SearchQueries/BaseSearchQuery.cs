using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.SearchQueries
{
    public abstract class BaseSearchQuery
    {
        public int Page { get; set; }
        public int Limit { get; set; } = 50;
    }
}
