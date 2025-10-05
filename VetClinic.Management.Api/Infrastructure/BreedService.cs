using VetClinic.Management.Domain;
using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Api.Infrastructure
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
               new Breed(Guid.Parse("abe7dbeb-095e-4d2e-a70d-d7ecd72cb786"), "Beagle", new WeightRange(12.5, 21.7), new WeightRange(9.9, 20)),
               new Breed(Guid.Parse("abe7dbeb-095e-4d2e-a70d-d7ecd72cb786"), "Husky", new WeightRange(15.3, 24.9), new WeightRange(12, 21.6))
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
