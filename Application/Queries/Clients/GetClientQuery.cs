using Application.Models;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Clients
{
    public class GetClientQuery : IRequest<ClientModel>
    {
        public int Id { get; set; }
    }

    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientModel>
    {
        private readonly IClientsService _clientService;

        public GetClientQueryHandler(IClientsService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientModel> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            return await _clientService.GetAsync(request.Id);
        }
    }
}
