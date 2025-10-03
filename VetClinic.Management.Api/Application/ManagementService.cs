using VetClinic.Management.Domain;
using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;
using VetClinic.SharedKernel.ValueObjects;

namespace VetClinic.Management.Api.Application
{
    public class ManagementService(IManagementRepository repository, IBreedService breedService)
    {
        public async Task Handle(CreatePetCommand command)
        {

            var breedId = new BreedId(command.BreedId, breedService);
            var newPet = new Pet(
                command.Id,
                command.Name, 
                command.Age, 
                command.Color,
                command.Gender,
                breedId);

            repository.Add(newPet);
        }

       
    }

}
