using DDD.Contracts._Common;
using DDD.DomainModels.People.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.DataAccess._Common
{
    public class ServiceDbContext : BaseDbContext
    {
        public virtual DbSet<Person> People { get; set; }

        private readonly IUnitOfWorkConfiguration _configuration;

        public ServiceDbContext(DbContextOptions<ServiceDbContext> options,
            IUnitOfWorkConfiguration configuration) : base(options)
            => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.SqlServerConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);

            CreateSequence(modelBuilder, nameof(Person));
        }

        private void CreateSequence(ModelBuilder modelBuilder, string name)
            => modelBuilder
                .HasSequence<int>(name + "_Sequence", "dbo")
                .StartsAt(1)
                .HasMin(1)
                .IncrementsBy(1);
    }
}