using MxFace.Fingerprint.SDK.Sample.Net.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MxFace.Fingerprint.SDK.Sample.Net.Extensions;

internal static class Extensions
{
    private const string DatabaseConnectionDefaultsSectionName = "DatabaseConnectionDefaults";

    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<MxFaceFingerprintSDKContext>((serviceProvider, options) =>
        {
            System.Diagnostics.Debug.WriteLine("MxIdentityTenancyDbContext::conn ->" + connectionString);

            options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });

        builder.AddMxFaceFingerprintServices();
    }
}
