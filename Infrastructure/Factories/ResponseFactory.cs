using Application.Factories;
using Application.Models.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public BaseResponseModel Create(ResponseStatuses status, string? message = null)
        {
            return new BaseResponseModel() 
            {
                Status = status.ToString(),
                Message = message
            };
        }

        public ResponseModel<T> Create<T>(ResponseStatuses status, string? message = null, T? item = null) where T : class
        {
            return new ResponseModel<T>()
            {
                Status = status.ToString(),
                Message = message,
                Item = item
            };
        }
    }
}
