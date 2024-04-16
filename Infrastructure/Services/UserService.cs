using Application.Commands.Users;
using Application.Factories;
using Application.Models;
using Application.Models.Responses;
using Application.Models.SearchQueries;
using Application.Persistance;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IResponseFactory _responseFactory;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IResponseFactory responseFactory, IMapper mapper)
        {
            _userManager = userManager;
            _responseFactory = responseFactory;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
                return _responseFactory.Create(ResponseStatuses.Success, "User deleted successfully");
            }

            return _responseFactory.Create(ResponseStatuses.Error, $"User with id - {id} was not found");
        }

        public async Task<ResponseModel<List<UserModel>>> GetAll(UsersSearchQuery query)
        {
            var usersQueryable = _userManager.Users.AsQueryable();

            if(!string.IsNullOrEmpty(query.UserName))
            {
                usersQueryable = usersQueryable.Where(x => x.UserName!.Contains(query.UserName));
            }

            var result = await usersQueryable.Skip(query.Page * query.Limit).Take(query.Limit).ToListAsync();

            return _responseFactory.Create(ResponseStatuses.Success, item: _mapper.Map<List<UserModel>>(result));
        }

        public async Task<ResponseModel<UserModel>> GetUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                return _responseFactory.Create<UserModel>(ResponseStatuses.Error);
            }

            return _responseFactory.Create(ResponseStatuses.Success, item: _mapper.Map<UserModel>(user));
        }

        public async Task<BaseResponseModel> UpdateAsync(PutUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            if(user == null)
            {
                return _responseFactory.Create(ResponseStatuses.Error, "User not found");
            }

            user.UserName = command.UserName;
            user.Email = command.Email;

            await _userManager.UpdateAsync(user);

            return _responseFactory.Create(ResponseStatuses.Success, "User updated successfully");
        }
    }
}
