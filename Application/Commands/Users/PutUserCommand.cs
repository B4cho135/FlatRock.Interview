using Application.Models;
using Application.Models.Responses;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class PutUserCommand : IRequest<BaseResponseModel>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }

    public class PutUserCommandHandler : IRequestHandler<PutUserCommand, BaseResponseModel>
    {
        private readonly IUserService _userService;

        public PutUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponseModel> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateAsync(request);
        }
    }
}
