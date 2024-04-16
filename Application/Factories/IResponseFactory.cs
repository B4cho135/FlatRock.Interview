using Application.Models.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IResponseFactory
    {
        BaseResponseModel Create(ResponseStatuses status, string? message = null);
        ResponseModel<T> Create<T>(ResponseStatuses status, string? message = null, T? item = null) where T : class;
    }
}
