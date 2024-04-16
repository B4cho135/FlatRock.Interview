using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Repositories
{
    public interface IVideoRecordingsRepository : IRepository<VideoRecordingEntity, int>
    {
        Task AddVideoRecording(byte[] recording, int clientId, string? sessionId);
    }
}
