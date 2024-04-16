using Application.Persistance;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class FlatRockDbContext : IdentityDbContext<IdentityUser>, IFlatRockDbContext
    {
        public FlatRockDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<VideoRecordingEntity> VideoRecords { get; set; }
        public DbSet<ClientRecordingEntity> ClientRecordings { get; set; }
        public DbSet<PhotoEntity> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
        }
    }
}
