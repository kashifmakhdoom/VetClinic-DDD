namespace VetClinic.Consultation.Domain.Tests
{
    public class Consultation_Tests
    {
        [Fact]
        public void Consultation_should_be_open()
        {
            // Arrange
            var consl = new Consultation(Guid.NewGuid());

            // Assert
            Assert.True(consl.Status == ConsultationStatus.Open);
        }

        [Fact]
        public void Consultation_should_not_have_ended_timestamp()
        {
            // Arrange
            var consl = new Consultation(Guid.NewGuid());

            // Assert
            Assert.Null(consl.EndedAt);
        }

        [Fact]
        public void Consultation_should_not_end_when_missing_data()
        {
            // Arrange
            var consl = new Consultation(Guid.NewGuid());

            // Assert
            Assert.Throws<InvalidOperationException>(() => consl.End());
        }

        [Fact]
        public void Consultation_should_end_when_data_complete()
        {
            // Arrange
            var consl = new Consultation(Guid.NewGuid());
            consl.SetTreatment("Treatment");
            consl.SetDiagnosis("Diagnosis");
            consl.SetWeight(18.5);
            consl.End();

            // Assert
            Assert.True(consl.Status == ConsultationStatus.Closed);
        }

        [Fact]
        public void Consultation_should_not_allow_weight_update_when_closed()
        {
            // Arrange
            var consl = new Consultation(Guid.NewGuid());
            consl.SetTreatment("Treatment");
            consl.SetDiagnosis("Diagnosis");
            consl.SetWeight(18.5);
            consl.End();

            // Assert
            Assert.Throws<InvalidOperationException>(() => consl.SetWeight(19.3));
        }
    }
}