using APP_SportHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP_SportHealth.Infrastructure
{

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityPoint> ActivityPoints { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("id");

                entity.Property(x => x.Name)
                    .HasColumnName("name");

                entity.Property(x => x.Email)
                    .HasColumnName("email");

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(x => x.PasswordHash)
                    .HasColumnName("password_hash");

                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activities");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.Distance).HasColumnName("distance");
                entity.Property(x => x.Duration).HasColumnName("duration");
                entity.Property(x => x.Calories).HasColumnName("calories");
                entity.Property(x => x.AvgPace).HasColumnName("avg_pace");
                entity.Property(x => x.StartedAt).HasColumnName("started_at");
                entity.Property(x => x.EndedAt).HasColumnName("ended_at");
            });

            modelBuilder.Entity<ActivityPoint>(entity =>
            {
                entity.ToTable("activity_points");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.ActivityId).HasColumnName("activity_id");
                entity.Property(x => x.Latitude).HasColumnName("latitude");
                entity.Property(x => x.Longitude).HasColumnName("longitude");
                entity.Property(x => x.Timestamp).HasColumnName("timestamp");

                entity.HasIndex(x => x.ActivityId);
            });
        }
    }
}
