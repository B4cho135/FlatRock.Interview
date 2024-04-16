using Application.Factories;
using Application.Models.Responses;
using Application.Persistance.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Photos
{
    public class PostPhotoCommand : IRequest<BaseResponseModel>
    {
        public string? Base64 { get; set; }
        public string? SessionId { get; set; }
    }

    public class PostPhotoCommandHandler : IRequestHandler<PostPhotoCommand, BaseResponseModel>
    {
        private readonly IRepository<PhotoEntity, int> _photosRepo;
        private readonly IResponseFactory _responseFactory;

        public PostPhotoCommandHandler(IRepository<PhotoEntity, int> photosRepo, IResponseFactory responseFactory)
        {
            _photosRepo = photosRepo;
            _responseFactory = responseFactory;
        }

        public async Task<BaseResponseModel> Handle(PostPhotoCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Base64))
            {
                return _responseFactory.Create(ResponseStatuses.Error, "File is empty");
            }

            try
            {
                var bytea = Convert.FromBase64String(request.Base64.Replace("data:image/png;base64,",""));

                await _photosRepo.CreateAsync(new PhotoEntity()
                {
                    Photo = bytea,
                    SessionId = request.SessionId,
                });

                return _responseFactory.Create(ResponseStatuses.Success);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
