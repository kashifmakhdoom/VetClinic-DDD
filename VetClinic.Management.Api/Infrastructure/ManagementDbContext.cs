using Microsoft.EntityFrameworkCore;
using VetClinic.Management.Domain.Entities;
using VetClinic.Management.Domain.ValueObjects;

namespace VetClinic.Management.Api.Infrastructure
{
    public class ManagementDbContext(DbContextOptions options) 
        : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Pet>()
               .Property(p => p.BreedId)
               .HasConversion(v => v.Value, v => BreedId.Create(v));

            modelBuilder.Entity<Pet>().OwnsOne(p => p.Weight, w =>
            {
                w.Property(w => w.Value).HasColumnName("Weight");
            });
        }
    }
}
