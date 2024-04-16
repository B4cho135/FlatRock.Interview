using Application.Commands.Users;
using Application.Models;
using Application.Models.Responses;
using Application.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<ResponseModel<UserModel>> GetUserAsync(Guid id);
        Task<ResponseModel<List<UserModel>>> GetAll(UsersSearchQuery query);
        Task<BaseResponseModel> UpdateAsync(PutUserCommand command);
    }
}
