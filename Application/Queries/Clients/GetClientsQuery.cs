using Application.Factories;
using Application.Models;
using Application.Models.Responses;
using Application.Services;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Clients
{
    public class GetClientsQuery : IRequest<ResponseModel<List<ClientModel>>>
    {
    }

    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ResponseModel<List<ClientModel>>>
    {
        private readonly IClientsService _clientsService;
        private readonly IResponseFactory _responseFactory;

        public GetClientsQueryHandler(IClientsService clientsService, IResponseFactory responseFactory)
        {
            _clientsService = clientsService;
            _responseFactory = responseFactory;
        }

        public async Task<ResponseModel<List<ClientModel>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientsService.GetAllAsync();

            return _responseFactory.Create(ResponseStatuses.Success, item: clients);
        }
    }
}
