
using VetClinic.Management.Api.Infrastructure;
using VetClinic.Management.Domain;
using VetClinic.Management.Domain.Events;

namespace VetClinic.Management.Api.Application
{
    public class SetWeightCommandHandler 
        : ICommandHandler<SetWeightCommand>
    {
        private readonly IManagementRepository repository;
        private readonly IBreedService breedService;

        public SetWeightCommandHandler(IManagementRepository repository, 
            IBreedService breedService)
        {
            this.repository = repository;
            this.breedService = breedService;

            DomainEvents.PetWeightUpdated.Subscribe((domainEvent) =>
            {
                // TODO: Send a message to the message broker
            });
        }
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
