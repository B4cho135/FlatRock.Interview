using Application.Factories;
using Application.Models.Responses;
using Application.Services;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Clients
{
    public class DeleteClientCommand : IRequest<BaseResponseModel>
    {
        public int Id { get; set; }

        public DeleteClientCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, BaseResponseModel>
    {
        private readonly IClientsService _clientService;
        private readonly IResponseFactory _responseFactory;

        public DeleteClientCommandHandler(IClientsService clientService, IResponseFactory responseFactory)
        {
            _clientService = clientService;
            _responseFactory = responseFactory;
        }

        public async Task<BaseResponseModel> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var result = await _clientService.DeleteAsync(request.Id);
            if(result)
            {
                return _responseFactory.Create(ResponseStatuses.Success);
            }

            return _responseFactory.Create(ResponseStatuses.Error, "Could not delete client");
        }
    }
}
