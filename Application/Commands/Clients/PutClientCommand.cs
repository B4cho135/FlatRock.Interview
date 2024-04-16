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
    public class PutClientCommand : IRequest<BaseResponseModel>
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public int? Age { get; set; }
        public int? UserId { get; set; }
    }

    public class PutClientCommandHandler : IRequestHandler<PutClientCommand, BaseResponseModel>
    {
        private readonly IClientsService _clientService;
        private readonly IMapper _mapper;
        private readonly IResponseFactory _responseFactory;

        public PutClientCommandHandler(IClientsService clientService, IMapper mapper, IResponseFactory responseFactory)
        {
            _clientService = clientService;
            _mapper = mapper;
            _responseFactory = responseFactory;
        }

        public async Task<BaseResponseModel> Handle(PutClientCommand request, CancellationToken cancellationToken)
        {
            await _clientService.UpdateAsync(_mapper.Map<ClientEntity>(request));
            return _responseFactory.Create(ResponseStatuses.Success);
        }
    }
}
