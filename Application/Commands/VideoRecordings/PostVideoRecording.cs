using Application.Factories;
using Application.Models.Responses;
using Application.Persistance;
using Application.Persistance.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Commands.VideoRecordings
{
    public class PostVideoRecording : IRequest<BaseResponseModel>
    {
        //[Required]
        //public IFormFile File { get; set; }
        public byte[] FileBytes { get; set; } = [];
        public string? SessionId { get; set; }
    }

    public class PostVideoRecordingHandler : IRequestHandler<PostVideoRecording, BaseResponseModel>
    {
        private readonly IVideoRecordingsRepository _videoRecordingsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ClientEntity, int> _clientsRepo;
        private readonly IResponseFactory _responseFactory;

        public PostVideoRecordingHandler(IVideoRecordingsRepository videoRecordingsRepository, IHttpContextAccessor httpContextAccessor, IRepository<ClientEntity, int> clientsRepo, IResponseFactory responseFactory)
        {
            _videoRecordingsRepository = videoRecordingsRepository;
            _httpContextAccessor = httpContextAccessor;
            _clientsRepo = clientsRepo;
            _responseFactory = responseFactory;
        }

        public async Task<BaseResponseModel> Handle(PostVideoRecording request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var client = _clientsRepo.GetDbSet()
                .Include(x => x.User)
                .FirstOrDefault(x => !x.IsDeleted && x.User.UserName == userName);

            if(client == null)
            {
                return _responseFactory.Create(ResponseStatuses.Error, "Could not find associated client");
            }

            await _videoRecordingsRepository.AddVideoRecording(request.FileBytes, client.Id, request.SessionId);

            return _responseFactory.Create(ResponseStatuses.Success);
        }
    }
}
