using VetClinic.Management.Domain;
using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Api.Infrastructure
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
               new Breed(Guid.Parse("abe7dbeb-095e-4d2e-a70d-d7ecd72cb786"), "Beagle", new WeightRange(12m, 22m), new WeightRange(10m, 20m)),
               new Breed(Guid.Parse("abe7dbeb-095e-4d2e-a70d-d7ecd72cb786"), "Husky", new WeightRange(15m, 25m), new WeightRange(12m, 22m))
            ];
        public Breed? GetBreed(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Breed is not valid!");

            var result = breeds.Find(breeds => breeds.Id == id);
            return result ?? throw new ArgumentException("Breed was not found!");
        }
    }
}
