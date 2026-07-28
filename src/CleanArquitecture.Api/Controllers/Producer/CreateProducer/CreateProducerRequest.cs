namespace CleanArquitecture.Api.Controllers.Producer.CreateProducer;

public record CreateProducerRequest(string Name, string Password, string Email, string Cnpj, string Description);
