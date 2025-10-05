
using VetClinic.SharedKernel;

namespace VetClinic.Management.Domain.Events
{
    public static class DomainEvents
    {
        public static DomainEventDispatcher<PetWeightUpdated> PetWeightUpdated = new();
    }
}
