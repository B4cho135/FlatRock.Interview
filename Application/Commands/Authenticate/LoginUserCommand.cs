using Application.Models.Responses.Authentication;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Authenticate
{
    public class LoginUserCommand : IRequest<OneOf<TokenResponse, string>>
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OneOf<TokenResponse, string>>
    {
        private readonly IAuthenticatonService _authService;

        public LoginUserCommandHandler(IAuthenticatonService authService)
        {
            _authService = authService;
        }

        public async Task<OneOf<TokenResponse, string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request);
        }
    }
}
