
using VetClinic.Management.Api.Infrastructure;
using VetClinic.Management.Domain;

namespace VetClinic.Management.Api.Application
{
    public class SetWeightCommandHandler(IManagementRepository repository, IBreedService breedService) 
        : ICommandHandler<SetWeightCommand>
    {
        public async Task Handle(SetWeightCommand command)
        {
            var pet = repository.GetById(command.Id);
            if (pet == null)
            {
                throw new Exception("Pet wast not found!");
            }
            pet.SetWeight(command.Weight, breedService);

            repository.Update(pet);

        }
    }
}
