using Application.Persistance.Repositories;
using Domain.Entities;
using Npgsql.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class VideoRecordingRepository : Repository<VideoRecordingEntity, int>, IVideoRecordingsRepository
    {
        public VideoRecordingRepository(FlatRockDbContext context) : base(context)
        {
        }

        public async Task AddVideoRecording(byte[] recording, int clientId, string? sessionId)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var videoRecording = await dbContext.VideoRecords.AddAsync(new VideoRecordingEntity()
                {
                    VideoRecording = recording,
                    SessionId = sessionId
                });

                await dbContext.ClientRecordings.AddAsync(new ClientRecordingEntity()
                {
                    VideoRecording = videoRecording.Entity,
                    ClientId = clientId
                });

                await dbContext.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}
