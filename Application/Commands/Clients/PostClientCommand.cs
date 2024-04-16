using Application.Factories;
using Application.Models.Responses;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Clients
{
    public class PostClientCommand : IRequest<BaseResponseModel>
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string? UserId { get; set; }
    }

    public class PostClientCommandHandler : IRequestHandler<PostClientCommand, BaseResponseModel>
    {
        private readonly IClientsService _clientService;
        private readonly IMapper _mapper;
        private readonly IResponseFactory _responseFactory;

        public PostClientCommandHandler(IClientsService clientService, IMapper mapper, IResponseFactory responseFactory)
        {
            _clientService = clientService;
            _mapper = mapper;
            _responseFactory = responseFactory;
        }

        public async Task<BaseResponseModel> Handle(PostClientCommand request, CancellationToken cancellationToken)
        {
            //Usually I have a factory interface/implementation for creating new entity
            //just like IResponseFactory, but in order to speed things up
            //I will directly map request to entity
            await _clientService.CreateAsync(_mapper.Map<ClientEntity>(request));

            return _responseFactory.Create(ResponseStatuses.Success);
        }
    }
}
