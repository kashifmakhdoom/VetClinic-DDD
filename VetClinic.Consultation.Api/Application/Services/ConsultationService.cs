using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using VetClinic.Consultation.Api.Application.Commands;
using VetClinic.Consultation.Api.Infrastructure;
using VetClinic.Consultation.Domain.Entities;
using VetClinic.Consultation.Domain.ValueObjects;
using VetClinic.SharedKernel;
using ClinicalConsultation = VetClinic.Consultation.Domain.Entities.Consultation;

namespace VetClinic.Consultation.Api.Application.Services
{
    public class ConsultationService(ConsultationDbContext dbContext)
    {
        public async Task<Guid> Handle(StartConsultationCommand command)
        {
            // *** Using EventSourcing pattern ***
            var newConsultation = new ClinicalConsultation(command.PatientId);
            await SaveAsync(newConsultation);
            return newConsultation.Id;

            #region *** Using Traditional pattern ***
            //await dbContext.Consultations.AddAsync(newConsultation);
            //await dbContext.SaveChangesAsync();
            //return newConsultation.Id;
            #endregion
        }

        public async Task Handle(EndConsultationCommand command)
        {
            // *** Using EventSourcing pattern ***
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.End();
            await SaveAsync(consultation);

            #region *** Using Traditional pattern ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.End();
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task Handle(SetDiagnosisCommand command)
        {
            // Using EventSourcing pattern
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.SetDiagnosis(command.Diagnosis);
            await SaveAsync(consultation);

            #region *** Using Traditional pattern ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.SetDiagnosis(command.Diagnosis);
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task Handle(SetTreatmentCommand command)
        {
            // *** Using EventSourcing pattern ***
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.SetTreatment(command.Treatment);
            await SaveAsync(consultation);

            #region *** Using Traditional pattern ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.SetTreatment(command.Treatment);
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task Handle(SetWeightCommand command)
        {
            // *** Using EventSourcing pattern ***
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.SetWeight(command.Weight);
            await SaveAsync(consultation);

            #region *** Using Traditional pattern ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.SetWeight(command.Weight);
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task Handle(AdministerDrugCommand command)
        {
            // *** Using EventSourcing pattern ***
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.AdministerDrug(command.DrugId,
                new Dose(command.Quantity, command.UnitOfMeasure));
            await SaveAsync(consultation);

            #region *** Using Traditional approach ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.AdministerDrug(command.DrugId,
            //new Dose(command.Quantity, command.UnitOfMeasure));
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task Handle(RegisterVitalSignsCommand command)
        {
            // *** Using EventSourcing pattern ***
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.RegisterVitalSigns(command.VitalSignReadings
                                                .Select(v => new VitalSigns(v.ReadingDateTime,
                                                                              v.Temperature,
                                                                              v.HeartRate,
                                                                             v.RespiratoryRate)));
            await SaveAsync(consultation);

            #region *** Using Traditional approach ***
            //var consultation = await dbContext.Consultations.FindAsync(command.ConsultationId);
            //consultation.RegisterVitalSigns(command.VitalSignReadings
            //                                     .Select(v => new VitalSigns(v.ReadingDateTime,
            //                                                                   v.Temperature,
            //                                                                   v.HeartRate,
            //                                                                   v.RespiratoryRate)));
            //await dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task SaveAsync(ClinicalConsultation consultation)
        {
            var aggregateId = $"Consultation-{consultation.Id.ToString()}";
            var changes = consultation.GetChanges()
                .Select(e => new ConsultationEventData(Guid.NewGuid(),
                            aggregateId,
                            e.GetType().Name,
                            JsonConvert.SerializeObject(e),
                            e.GetType().AssemblyQualifiedName ?? string.Empty));

            if (!changes.Any())
            {
                return;
            }

            foreach (var change in changes)
            {
                await dbContext.Consultations.AddAsync(change);
            }

            await dbContext.SaveChangesAsync();

            consultation.ClearChanges();
        }

        public async Task<ClinicalConsultation> LoadAsync(Guid id)
        {
            var aggregateId = $"Consultation-{id}";
            var result = await dbContext.Consultations
                .Where(a => a.AggregateId == aggregateId)
                .ToListAsync();

            var domainEvents = result.Select(e =>
            {
                var assemblyQualifiedName = e.AssemblyQualifiedName;
                var eventType = Type.GetType(assemblyQualifiedName);
                var data = JsonConvert.DeserializeObject(e.Data, eventType!);
                return data as IDomainEvent;
            });

            var aggregate = new ClinicalConsultation(domainEvents!);

            return aggregate;
        }
    }
}
