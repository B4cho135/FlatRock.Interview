using Application.Models;
using Application.Models.Responses;
using Application.Models.SearchQueries;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUsersQuery : IRequest<ResponseModel<List<UserModel>>>
    {
        public UsersSearchQuery SearchQuery { get; set; } = new();
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResponseModel<List<UserModel>>>
    {
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResponseModel<List<UserModel>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAll(request.SearchQuery);
        }
    }
}
