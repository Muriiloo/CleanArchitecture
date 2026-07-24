using CleanArquitecture.Domain.Entities.Customer;
using CleanArquitecture.Domain.Entities.Customer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitecture.Infraestructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");
        builder.HasKey(x => x.Id);

        
        builder.Property(x => x.Id)
            .ValueGeneratedNever() // dizendo para não gerar o ID automaticamente, pois vai ser gerado pelo dominio
            .HasConversion(x => x.Value, value => new CustomerId(value)); // convertento pois o EF não sabe o que é um customerId, quando enviar vai pegar o valor do guid com o .Value e qunado fazer uma consulta no banco de dados vai devolver um customerId

        builder.Property(x => x.FullName)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, value => FullName.FromPersistence(value));

        builder.Property(x => x.Password)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, value => Password.FromPersistence(value));

        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .HasConversion(x => x.Value, value => Email.FromPersistence(value));

        builder.Property(x => x.Cpf)
            .HasMaxLength(11)
            .HasConversion(x => x.Value, value => Cpf.FromPersistence(value));

        builder.Property(x => x.BirthDay)
            .HasMaxLength(10)
            .HasConversion(x => x.Value, value => BirthDay.FromPersistence(value));

    }
}
