using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class BaseResponseModel
    {
        public string Status { get; set; } = ResponseStatuses.Success.ToString();
        public string? Message { get; set; }
    }
}
