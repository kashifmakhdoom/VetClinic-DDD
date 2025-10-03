
using Microsoft.EntityFrameworkCore;
using VetClinic.Management.Api.Application;
using VetClinic.Management.Api.Infrastructure;
using VetClinic.Management.Domain;

namespace VetClinic.Management.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IManagementRepository, ManagementRepository>();
            builder.Services.AddScoped<IBreedService, BreedService>();
            
            builder.Services.AddScoped<ICommandHandler<SetWeightCommand>, SetWeightCommandHandler>();

            builder.Services.AddDbContext<ManagementDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("LocalDbConnection"));
            });

            var app = builder.Build();

            app.EnsureDbIsCreated();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
