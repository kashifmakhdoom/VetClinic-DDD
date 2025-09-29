using VetClinic.SharedKernel;
using VetClinic.Consultation.Domain.ValueObjects;
using VetClinic.SharedKernel.ValueObjects;

namespace VetClinic.Consultation.Domain
{
    public class Consultation : AggregatRoot
    {

        private readonly List<DrugAdministration> _administeredDrugs = new();
        private readonly List<VitalSigns> _vitalSignsReading = new();
        public DateTime StartedAt { get; init; }
     
        public DateTime? EndedAt { get; private set; }
        public Text Diagnosis { get; private set; }
        public Text Treatment { get; private set; }
        public PatientId PatientId { get; init; }
        public Weight CurrentWeight { get; private set; }
        public ConsultationStatus Status { get; private set; }

        public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => _administeredDrugs;
        public IReadOnlyCollection<VitalSigns> VitalSignsReading => _vitalSignsReading;
        public Consultation(PatientId patientId)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Status = ConsultationStatus.Open;
            StartedAt = DateTime.UtcNow;
        }

        public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
        {
            ValidateConsultationStatus();
            _vitalSignsReading.AddRange(vitalSigns);
        }

        public void AdministerDrug(DrugId drugId, Dose dose)
        {
            ValidateConsultationStatus();
            var newDrugAdministration = new DrugAdministration(drugId, dose);
            _administeredDrugs.Add(newDrugAdministration);
        }

        public void End()
        {
            ValidateConsultationStatus();

            if(Diagnosis is null || Treatment is null || CurrentWeight is null)
            {
                throw new InvalidOperationException("The consulation cannot be ended!");
            }

            Status = ConsultationStatus.Closed;
            EndedAt = DateTime.UtcNow;
        }

        public void SetWeight(Weight weight)
        {
            ValidateConsultationStatus();
            this.CurrentWeight = weight;
        }
        public void SetDiagnosis(Text diagnosis)
        {
            ValidateConsultationStatus();
            this.Diagnosis = diagnosis;
        }
        public void SetTreatment(Text treatment)
        {
            ValidateConsultationStatus();
            this.Treatment = treatment;
        }

        private void ValidateConsultationStatus()
        {
            if(Status == ConsultationStatus.Closed)
            {
                throw new InvalidOperationException("The consulation is already closed!");
            }
        }
    }

    public enum ConsultationStatus
    {
        Open = 1,
        Closed = 2
    }
}
