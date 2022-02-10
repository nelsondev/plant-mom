using Microsoft.EntityFrameworkCore;

namespace Plant.Mom.Api.Entities;

public class PlantContext : DbContext
{
    public PlantContext(DbContextOptions<PlantContext> options) : base(options) 
        => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=plant.db");
    }

    public DbSet<MomConfiguration> Moms { get; set; }
    public DbSet<HumidityConfiguration> HumidityConfigurations { get; set; }
    public DbSet<LightingConfiguration> LightingConfigurations { get; set; }
    public DbSet<HumidityDaySchedule> HumidityDaySchedules { get; set; }
    public DbSet<HumidityDaySchedule> HumidityTimeSchedules { get; set; }
    public DbSet<LightingDaySchedule> LightingDaySchedules { get; set; }
    public DbSet<LightingDaySchedule> LightingTimeSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MomConfiguration>();
        modelBuilder.Entity<HumidityConfiguration>();
        modelBuilder.Entity<LightingConfiguration>();
        modelBuilder.Entity<HumidityDaySchedule>();
        modelBuilder.Entity<HumidityTimeSchedule>();
        modelBuilder.Entity<LightingDaySchedule>();
        modelBuilder.Entity<LightingTimeSchedule>();
    }
}
