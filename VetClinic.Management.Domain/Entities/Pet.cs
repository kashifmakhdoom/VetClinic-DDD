using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Domain.Entities
{
    public class Pet : Entity
    {
        public string Name { get; init; }
        public int Age { get; init; }
        public string Color { get; init; } 
        public Weight Weight { get; private set; }
        public WeightClass WeightClass { get; private set; }
        public GenderOfPet GenderOfPet { get; init; }
        public BreedId BreedId { get; init; }

        public Pet(Guid id,
                   string name,
                   int age,
                   string color,
                   GenderOfPet genderOfPet,
                   BreedId breedId)
        {
            Id = id;
            Name = name;
            Age = age;
            Color = color;
            GenderOfPet = genderOfPet;
            BreedId = breedId;
        }

        public void SetWeight(Weight weight, IBreedService breedService)
        {
            Weight = weight;
            SetWeightClass(breedService);
        }

        public void SetWeightClass(IBreedService breedService)
        {
            var desiredBreed = breedService.GetBreed(BreedId.Value);

            var (from, to) = GenderOfPet switch
            {
                GenderOfPet.Male => (desiredBreed.MaleIdealWeight.From, desiredBreed.MaleIdealWeight.To),
                GenderOfPet.Female => (desiredBreed.FemaleIdealWeight.From, desiredBreed.FemaleIdealWeight.To),
                _ => throw new NotImplementedException()
            };

            WeightClass = Weight.Value switch
            {
                _ when Weight.Value < from => WeightClass.Underweight,
                _ when Weight.Value > to => WeightClass.Overweight,
                _ => WeightClass.Ideal
            };
        }
    }

    public enum GenderOfPet
    {
        Male = 1,
        Female = 2
    }

    public enum WeightClass
    {
        Unknown = 0,
        Ideal = 1,
        Underweight = 2,
        Overweight = 3
    }
}
