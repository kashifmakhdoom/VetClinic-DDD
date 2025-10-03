using VetClinic.SharedKernel.ValueObjects;

namespace VetClinic.Management.Api.Application
{
    public record SetWeightCommand(Guid Id, double Weight);
    
}
