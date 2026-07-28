using CleanArquitecture.Domain.Entities.Producer;
using CleanArquitecture.Domain.Entities.Producer.ValueObjects;
using CleanArquitecture.Domain.Entities.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitecture.Infraestructure.Configurations;

public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
{
    public void Configure(EntityTypeBuilder<Producer> builder)
    {
        builder.ToTable("producers");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(x => x.Value, x => new ProducerId(x));

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, x => Name.FromPersistence(x));

        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => Description.FromPersistence(x));

        builder.Property(p => p.Email)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, x => Email.FromPersistence(x));

        builder.Property(p => p.Password)
            .HasMaxLength(50)
            .HasConversion(p => p.Value, p => Password.FromPersistence(p));

        builder.Property(p => p.Cnpj)
            .HasMaxLength(14)
            .HasConversion(p => p.Value, p => Cnpj.FromPersistence(p));


    }
}