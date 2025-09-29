using VetClinic.SharedKernel;
using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Domain.Entities
{
    public class Breed : Entity
    {
        public string Name { get; set; }
        public WeightRange MaleIdealWeight { get; set; }
        public WeightRange FemaleIdealWeight { get; set; }

        public Breed(Guid id, string name,
                     WeightRange maleIdeanWeight,
                     WeightRange femaleIdealWeight)
        {
            Id = id;
            Name = name;
            MaleIdealWeight = maleIdeanWeight;
            FemaleIdealWeight = femaleIdealWeight;
        }
    }
}
