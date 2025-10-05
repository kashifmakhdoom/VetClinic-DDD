using VetClinic.SharedKernel;
using VetClinic.Consultation.Domain.ValueObjects;
using VetClinic.SharedKernel.ValueObjects;
using VetClinic.Consultation.Domain.Events;

namespace VetClinic.Consultation.Domain.Entities
{
    public class Consultation : AggregateRoot
    {
        private readonly List<DrugAdministration> administeredDrugs = new();
        private readonly List<VitalSigns> vitalSignReadings = new();
        public DateTimeRange When { get; private set; }
        public Text? Diagnosis { get; private set; }
        public Text? Treatment { get; private set; }
        public PatientId PatientId { get; private set; }
        public Weight? CurrentWeight { get; private set; }
        public ConsultationStatus Status { get; private set; }
        public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administeredDrugs;
        public IReadOnlyCollection<VitalSigns> VitalSignReadings => vitalSignReadings;

        public Consultation(PatientId patientId)
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new ConsultationStarted(Guid.NewGuid(),
                                                     patientId,
                                                     DateTime.UtcNow));

            #region *** Using Traditional pattern ***
            /*
            Id = Guid.NewGuid();
            PatientId = patientId;
            Status = ConsultationStatus.Open;
            When = DateTime.UtcNow;
            */
            #endregion
        }

        public Consultation(IEnumerable<IDomainEvent> domainEvents)
        {
            Load(domainEvents);
        }

        public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
        {
            
            ValidateConsultationStatus();
            vitalSignReadings.AddRange(vitalSigns);
        }

        public void AdministerDrug(DrugId drugId, Dose dose)
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new DrugAdministrationUpdated(drugId, dose));

            #region *** Using Traditional approach ***
            ValidateConsultationStatus();
            var newDrugAdministration = new DrugAdministration(drugId, dose);
            administeredDrugs.Add(newDrugAdministration);
            #endregion
        }

        public void End()
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new ConsultationEnded(Id, DateTime.UtcNow));

            #region *** Using Traditional approach ***
            /*
            ValidateConsultationStatus();

            if (Diagnosis == null || Treatment == null || CurrentWeight == null)
            {
                throw new InvalidOperationException("The consultation cannot be ended.");
            }

            Status = ConsultationStatus.Closed;
            When = new DateTimeRange(When.StartedAt, DateTime.UtcNow);
            */
            #endregion
        }

        public void SetWeight(Weight weight)
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new WeightUpdated(Id, weight));

            #region *** Using Traditional approach ***
            /*
            ValidateConsultationStatus();
            CurrentWeight = weight;
            */
            #endregion
        }

        public void SetDiagnosis(Text diagnosis)
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new DiagnosisUpdated(Id, diagnosis));

            #region *** Using Traditional approach ***
            /*
            ValidateConsultationStatus();
            Diagnosis = diagnosis;
            */
            #endregion
        }

        public void SetTreatment(Text treatment)
        {
            // *** Using EventSourcing pattern ***
            ApplyDomainEvent(new TreatmentUpdated(Id, treatment));

            #region *** Using Traditional approach ***
            /*
            ValidateConsultationStatus();
            Treatment = treatment;
            */
            #endregion
        }

        private void ValidateConsultationStatus()
        {
            if (Status == ConsultationStatus.Closed)
            {
                throw new InvalidOperationException("The consultation is already closed.");
            }
        }

        protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case ConsultationStarted e:
                    Id = e.Id;
                    PatientId = e.PatientId;
                    When = new DateTimeRange(e.StartedAt);
                    Status = ConsultationStatus.Open;
                    break;
                case DiagnosisUpdated e:
                    ValidateConsultationStatus();
                    Diagnosis = e.Diagnosis;
                    break;
                case TreatmentUpdated e:
                    ValidateConsultationStatus();
                    Treatment = e.Treatment;
                    break;
                case WeightUpdated e:
                    ValidateConsultationStatus();
                    CurrentWeight = e.Weight;
                    break;
                case DrugAdministrationUpdated e:
                    ValidateConsultationStatus();
                    var newDrugAdministration = new DrugAdministration(e.drugId, e.dose);
                    administeredDrugs.Add(newDrugAdministration);
                    break;
                case ConsultationEnded e:
                    ValidateConsultationStatus();

                    if (Diagnosis == null || Treatment == null || CurrentWeight == null)       
                        throw new InvalidOperationException("The consultation cannot be ended.");
                    
                    Status = ConsultationStatus.Closed;
                    When = new DateTimeRange(When.StartedAt, e.EndedAt);
                    break;
            }
        }
    }

    public enum ConsultationStatus
    {
        Open = 1,
        Closed = 2
    }
}
