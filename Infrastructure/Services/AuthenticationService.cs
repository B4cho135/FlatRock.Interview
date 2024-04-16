using Application.Commands.Authenticate;
using Application.Commands.Users;
using Application.Factories;
using Application.Models;
using Application.Models.Responses;
using Application.Models.Responses.Authentication;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticatonService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IResponseFactory _responseFactory;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IResponseFactory responseFactory, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _responseFactory = responseFactory;
            _mapper = mapper;
        }

        public async Task<OneOf<TokenResponse, string>> LoginAsync(LoginUserCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
            }

            return "Username or password is incorrect";
        }

        public async Task<ResponseModel<UserModel>> RegisterAsync(RegisterUserCommand command)
        {
            var userExists = await _userManager.FindByNameAsync(command.Username);
            if (userExists != null)
                return _responseFactory.Create<UserModel>(ResponseStatuses.Error, "User already exists!");

            IdentityUser user = new()
            {
                Email = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = command.Username
            };
            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var errorMessages = string.Empty;

                foreach (var item in result.Errors)
                {
                    errorMessages += item.Description + " /n";
                }
                return _responseFactory.Create<UserModel>(ResponseStatuses.Error, errorMessages);
            }

            if(command.IsAdmin)
            {
                var adminRole = Roles.Admin.ToString();

                if (!await _roleManager.RoleExistsAsync(adminRole))
                    await _roleManager.CreateAsync(new IdentityRole(adminRole));

                if (await _roleManager.RoleExistsAsync(adminRole))
                {
                    await _userManager.AddToRoleAsync(user, adminRole);
                }
            }

            return _responseFactory.Create<UserModel>(ResponseStatuses.Success, item: _mapper.Map<UserModel>(user));
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
