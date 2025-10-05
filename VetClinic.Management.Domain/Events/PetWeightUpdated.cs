using VetClinic.SharedKernel;

namespace VetClinic.Management.Domain.Events
{
    public record PetWeightUpdated(Guid Id, double Weight) : IDomainEvent;
}
