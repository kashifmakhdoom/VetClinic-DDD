using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Domain
{
    public interface IBreedService    {

        Breed? GetBreed(Guid id);
    }

    public class FakeBreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
               new Breed(Guid.NewGuid(), "Beagle", new WeightRange(12m, 22m), new WeightRange(10m, 20m)),
               new Breed(Guid.NewGuid(), "Husky", new WeightRange(15m, 25m), new WeightRange(12m, 22m))
            ];
        public Breed? GetBreed(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Breed is not valid!");

            var result = breeds.Find(breeds =>  breeds.Id == id);
            return result ?? throw new ArgumentException("Breed was not found!");
        }
    }
}
