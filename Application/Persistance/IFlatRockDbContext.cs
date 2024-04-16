using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance
{
    public interface IFlatRockDbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<VideoRecordingEntity> VideoRecords { get; set; }
        public DbSet<ClientRecordingEntity> ClientRecordings { get; set; }
    }
}
