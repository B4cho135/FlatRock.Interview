using Application.Factories;
using Application.Models;
using Application.Models.Responses;
using Application.Persistance.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dashboard
{
    public class GetStatisticsQuery : IRequest<ResponseModel<StatisticsModel>>
    {
        public DateTime? LowerBoundary { get; set; }
        public DateTime? UpperBoundary { get; set; }
    }

    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, ResponseModel<StatisticsModel>>
    {
        private readonly IRepository<VideoRecordingEntity, int> _videoRecordingsRepo;
        private readonly IRepository<PhotoEntity, int> _photosRepo;
        private readonly IResponseFactory _responseFactory;

        public GetStatisticsQueryHandler(IRepository<VideoRecordingEntity, int> videoRecordingsRepo, IRepository<PhotoEntity, int> photosRepo, IResponseFactory responseFactory)
        {
            _videoRecordingsRepo = videoRecordingsRepo;
            _photosRepo = photosRepo;
            _responseFactory = responseFactory;
        }

        public async Task<ResponseModel<StatisticsModel>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var result = new StatisticsModel();

            var recordingsQueryable = _videoRecordingsRepo.GetDbSet().Where(x => !x.IsDeleted).AsQueryable();

            if (request.LowerBoundary.HasValue)
            {
                recordingsQueryable = recordingsQueryable.Where(x => x.CreatedAt >= request.LowerBoundary.Value);
            }


            if (request.UpperBoundary.HasValue)
            {
                recordingsQueryable = recordingsQueryable.Where(x => x.CreatedAt <= request.UpperBoundary.Value);
            }

            var photosQueryable = _photosRepo.GetDbSet().Where(x => !x.IsDeleted).AsQueryable();

            if (request.LowerBoundary.HasValue)
            {
                photosQueryable = photosQueryable.Where(x => x.CreatedAt >= request.LowerBoundary.Value);
            }


            if (request.UpperBoundary.HasValue)
            {
                photosQueryable = photosQueryable.Where(x => x.CreatedAt <= request.UpperBoundary.Value);
            }

            var recordingSessions = recordingsQueryable.Where(x => x.SessionId != null).Select(x => x.SessionId).Distinct().ToList();
            var photoSessions = photosQueryable.Where(x => x.SessionId != null).Select(x => x.SessionId).Distinct().ToList();

            var sessions = recordingSessions.Union(photoSessions).Distinct().Count();


            result.PerformedRecordings = recordingsQueryable.Count();
            result.ScreenCaptures = photosQueryable.Count();
            result.Sessions = sessions;

            return _responseFactory.Create(ResponseStatuses.Success, item: result);
            
        }
    }
}
