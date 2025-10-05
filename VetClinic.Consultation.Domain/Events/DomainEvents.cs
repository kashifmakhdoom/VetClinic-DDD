using VetClinic.Consultation.Domain.ValueObjects;
using VetClinic.SharedKernel;

namespace VetClinic.Consultation.Domain.Events
{
    public record ConsultationStarted(Guid Id, Guid PatientId, DateTime StartedAt) : IDomainEvent;
    public record DiagnosisUpdated(Guid Id, string Diagnosis) : IDomainEvent;
    public record TreatmentUpdated(Guid Id, string Treatment) : IDomainEvent;
    public record WeightUpdated(Guid Id, double Weight) : IDomainEvent;

    public record DrugAdministrationUpdated(DrugId drugId, Dose dose) : IDomainEvent;

    public record ConsultationEnded(Guid Id, DateTime EndedAt) : IDomainEvent;
}
