using Application.Commands.Clients;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Profiles
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<IdentityUser, UserModel>();

            CreateMap<ClientEntity, ClientModel>();
            CreateMap<PostClientCommand, ClientEntity>();
            CreateMap<PutClientCommand, ClientEntity>();
        }
    }
}
