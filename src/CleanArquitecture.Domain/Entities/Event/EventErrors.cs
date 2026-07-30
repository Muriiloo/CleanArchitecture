using CleanArquitecture.Domain.Abstrations;

namespace CleanArquitecture.Domain.Entities.Event;

public static class EventErrors
{
    public static Error InvalidNameEvent => new("400", "The name is invalid");
    public static Error InvalidDescriptionEvent => new("400", "The description is invalid");
    public static Error InvalidAddressEvent => new("400", "Invalid location.");
    public static Error InvalidPeriodEvent => new("400", "Invalid date.");
    public static Error InvalidLimitAge => new("401", "Under the minimum age.");
}