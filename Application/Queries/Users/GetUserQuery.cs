using Application.Models;
using Application.Models.Responses;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUserQuery : IRequest<ResponseModel<UserModel>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ResponseModel<UserModel>>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResponseModel<UserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserAsync(request.Id);
        }
    }
}
