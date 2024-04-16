using Application.Models;
using Application.Persistance.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ClientsService : GenericService<ClientEntity, ClientModel, int>, IClientsService
    {
        public ClientsService(IRepository<ClientEntity, int> entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}
