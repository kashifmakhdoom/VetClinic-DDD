using VetClinic.Consultation.Domain.ValueObjects;
using VetClinic.SharedKernel;

namespace VetClinic.Consultation.Domain.Entities
{
    public class DrugAdministration : Entity
    {
        public DrugId DrugId { get; init; }
        public Dose Dose { get; init; }

        public DrugAdministration(DrugId drugId, Dose dose)
        {
            Id = Guid.NewGuid();
            DrugId = drugId;
            Dose = dose;
        }

        public DrugAdministration()
        {

        }
    }
}
