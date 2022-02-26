using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YQB.DomainModels.People.Entities;
using YQB.DomainModels.People.Enums;
using YQB.DomainModels.People.ValueObjects;

namespace YQB.Infra.Data.People;

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

        builder.Property(s => s.Gender)
            .HasConversion(new EnumToStringConverter<Gender>())
            .HasMaxLength(100)
            .IsRequired();
    }
}