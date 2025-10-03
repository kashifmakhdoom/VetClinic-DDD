using Microsoft.EntityFrameworkCore;
using VetClinic.Consultation.Domain.ValueObjects;

using ConsultationEntity = VetClinic.Consultation.Domain.Entities.Consultation;

namespace VetClinic.Consultation.Api.Infrastructure
{
    public class ConsultationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ConsultationEntity> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ConsultationEntity>(consultation =>
            {
                consultation.HasKey(x => x.Id);

                consultation.Property(p => p.PatientId)
                            .HasConversion(v => v.Value, v => new PatientId(v));

                consultation.OwnsOne(p => p.Diagnosis);
                consultation.OwnsOne(p => p.Treatment);
                consultation.OwnsOne(p => p.CurrentWeight);
                consultation.OwnsOne(p => p.When);

                consultation.OwnsMany(c => c.AdministeredDrugs, a =>
                {
                    a.WithOwner().HasForeignKey("ConsultationId");
                    a.OwnsOne(d => d.DrugId);
                    a.OwnsOne(d => d.Dose);
                });

                consultation.OwnsMany(c => c.VitalSignReadings, v =>
                {
                    v.WithOwner().HasForeignKey("ConsultationId");
                });
            });
        }
    }

    public static class ClinicDbContextExtensions
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ConsultationDbContext>();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}

