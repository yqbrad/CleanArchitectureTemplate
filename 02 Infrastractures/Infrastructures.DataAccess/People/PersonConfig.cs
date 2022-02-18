using DDD.DomainModels.People.Entities;
using DDD.DomainModels.People.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.DataAccess.People
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.Property(s => s.FirstName)
                .HasConversion(s => s.Value, s => PersonFirstName.Create(s))
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.LastName)
                .HasConversion(s => s.Value, s => PersonLastName.Create(s))
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.Age)
                .HasConversion(s => s.Value, s => PersonAge.Create(s))
                .IsRequired();
        }
    }
}
