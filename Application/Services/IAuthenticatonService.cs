using Application.Commands.Authenticate;
using Application.Commands.Users;
using Application.Models;
using Application.Models.Responses;
using Application.Models.Responses.Authentication;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthenticatonService
    {
        Task<OneOf<TokenResponse, string>> LoginAsync(LoginUserCommand command);
        Task<ResponseModel<UserModel>> RegisterAsync(RegisterUserCommand command);

    }
}
