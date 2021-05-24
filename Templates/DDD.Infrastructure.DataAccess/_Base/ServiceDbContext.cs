using DDD.Contracts._Base;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$._Base
{
    public class ServiceDbContext: BaseDbContext
    {
        #region DbSets
        //public virtual DbSet<App> App { get; set; } 
        #endregion

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

            //CreateSequence(modelBuilder, nameof(App));
        }

        private void CreateSequence(ModelBuilder modelBuilder, string name)
            => modelBuilder
                .HasSequence<int>(name + "_Sequence", "dbo")
                .StartsAt(1)
                .HasMin(1)
                .IncrementsBy(1);
    }
}