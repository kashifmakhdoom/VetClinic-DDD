using VetClinic.Management.Domain.Entities;

namespace VetClinic.Management.Api.Application
{
    public record CreatePetCommand(Guid Id,
                                   string Name,
                                   int Age,
                                   string Color,
                                   GenderOfPet Gender,
                                   Guid BreedId);

}
