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
               new Breed(Guid.NewGuid(), "Beagle", new WeightRange(12, 22), new WeightRange(10, 20)),
               new Breed(Guid.NewGuid(), "Husky", new WeightRange(15, 25), new WeightRange(12, 22))
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
