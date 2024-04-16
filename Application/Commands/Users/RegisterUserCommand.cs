using Application.Models;
using Application.Models.Responses;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<ResponseModel<UserModel>>
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel<UserModel>>
    {
        private readonly IAuthenticatonService _authService;

        public RegisterUserCommandHandler(IAuthenticatonService authService)
        {
            _authService = authService;
        }
        public async Task<ResponseModel<UserModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request);
        }
    }
}
