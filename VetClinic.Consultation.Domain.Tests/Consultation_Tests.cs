using VetClinic.Consultation.Domain.Entities;
using VetClinic.Consultation.Domain.ValueObjects;
using ClinicalConsultation = VetClinic.Consultation.Domain.Entities.Consultation;

namespace VetClinic.Consultation.Domain.Tests
{
    public class Consultation_Tests
    {
        [Fact]
        public void Consultation_should_be_open()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            Assert.True(c.Status == ConsultationStatus.Open);
        }

        [Fact]
        public void Consultation_should_not_have_ended_timestamp()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            Assert.Null(c.When.EndedAt);
        }

        [Fact]
        public void Consultation_should_not_end_when_missing_data()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            Assert.Throws<InvalidOperationException>(c.End);
        }

        [Fact]
        public void Consultation_should_end_with_complete_data()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            c.SetTreatment("Treatment");
            c.SetDiagnosis("Diagnosis");
            c.SetWeight(18.5);
            c.End();
            Assert.True(c.Status == ConsultationStatus.Closed);
        }

        [Fact]
        public void Consultation_should_not_allow_weight_updates_when_closed()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            c.SetTreatment("Treatment");
            c.SetDiagnosis("Diagnosis");
            c.SetWeight(18.5);
            c.End();
            Assert.Throws<InvalidOperationException>(() => c.SetWeight(19.2));
        }

        [Fact]
        public void Consultation_should_not_allow_diagnosis_updates_when_closed()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            c.SetTreatment("Treatment");
            c.SetDiagnosis("Diagnosis");
            c.SetWeight(18.5);
            c.End();
            Assert.Throws<InvalidOperationException>(() => c.SetDiagnosis("New diagnosis"));
        }

        [Fact]
        public void Consultation_should_not_allow_treatment_updates_when_closed()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            c.SetTreatment("Treatment");
            c.SetDiagnosis("Diagnosis");
            c.SetWeight(18.5);
            c.End();
            Assert.Throws<InvalidOperationException>(() => c.SetTreatment("New treatment"));
        }

        [Fact]
        public void Consultation_should_administer_drug()
        {
            var drugId = new DrugId(Guid.NewGuid());
            var c = new ClinicalConsultation(Guid.NewGuid());
            c.AdministerDrug(drugId, new Dose(1, Dose.UnitOfMeasure.tablet));
            Assert.True(c.AdministeredDrugs.Count == 1);
            Assert.True(c.AdministeredDrugs.First().DrugId == drugId);
        }

        [Fact]
        public void Consultation_should_register_vitalsigns()
        {
            var c = new ClinicalConsultation(Guid.NewGuid());
            IEnumerable<VitalSigns> vitalSigns = [new VitalSigns(DateTime.UtcNow, 38.8m, 100, 120)];
            c.RegisterVitalSigns(vitalSigns);
            Assert.True(c.VitalSignReadings.Count == 1);
            Assert.True(c.VitalSignReadings.First() == vitalSigns.First());
        }

        [Fact]
        public void DateTimeRange_should_be_valid()
        {
            var theDate = new DateTime(2027, 12, 24, 22, 0, 0);
            var dr1 = new DateTimeRange(theDate, theDate.AddMinutes(10));
            Assert.NotNull(dr1.Duration);
        }

        [Fact]
        public void DateTimeRange_should_not_be_valid()
        {
            var theDate = new DateTime(2027, 12, 24, 22, 0, 0);
            Assert.Throws<InvalidOperationException>(() =>
            {
                var dr1 = new DateTimeRange(theDate.AddMinutes(10), theDate);
            });

        }

        [Fact]
        public void DateTimeRange_should_be_ongoing()
        {
            var theDate = new DateTime(2027, 12, 24, 22, 0, 0);
            var dr1 = new DateTimeRange(theDate);
            Assert.Equal("Ongoing", dr1.Duration);
        }

        [Fact]
        public void DateTimeRange_should_be_equals()
        {
            var theDate = new DateTime(2027, 12, 24, 22, 0, 0);
            var dr1 = new DateTimeRange(theDate);
            var dr2 = new DateTimeRange(theDate);
            Assert.Equal(dr1, dr2);
        }
    }
}