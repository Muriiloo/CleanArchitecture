using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Producer;

public static class ProducerErrors
{
    public static Error InvalidCnpj = new("400", "Invalid CNPJ");
    public static Error InvalidName = new("400", "Invalid name");
    public static Error InvalidDescription = new("400", "Invalid description");
    public static Error CnpjAlreadyExists = new("409", "Cnpj already exists.");
}