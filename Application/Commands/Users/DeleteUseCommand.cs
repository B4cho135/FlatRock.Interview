using Application.Models.Responses;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class DeleteUseCommand : IRequest<BaseResponseModel>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUseCommandHandler : IRequestHandler<DeleteUseCommand, BaseResponseModel>
    {
        private readonly IUserService _userService;

        public DeleteUseCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponseModel> Handle(DeleteUseCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteAsync(request.Id);
        }
    }
}
