using Microsoft.EntityFrameworkCore;
using VetClinic.Management.Api.Infrastructure;

public static class ManagementDbContextExtensions
{
    public static void EnsureDbIsCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ManagementDbContext>();

        dbContext.Database.EnsureCreated();
        dbContext.Database.CloseConnection();
    }
}
