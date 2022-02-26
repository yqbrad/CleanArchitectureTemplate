using Microsoft.EntityFrameworkCore;
using YQB.Contracts._Common;
using YQB.DomainModels.People.Entities;

namespace YQB.Infra.Data._Common
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