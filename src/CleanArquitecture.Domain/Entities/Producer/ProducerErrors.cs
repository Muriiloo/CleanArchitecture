using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Producer;

public static class ProducerErrors
{
    public static Error InvalidCnpj = new("invalid.cnpj", "Invalid CNPJ");
    public static Error InvalidName = new("invalid.name", "Invalid name");
}