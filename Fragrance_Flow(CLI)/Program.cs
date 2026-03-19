using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Fragrance_flow_DL_VERSION_.Application.Services;
using Fragrance_flow_DL_VERSION_.Infrastructure.Repositories;

using Fragrance_flow_DL_VERSION_.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
// TIME WASTED (Including writting the 1000 line prototype) : 100h 0m. GAWDDAMN
public class Fragrance_Flow
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

        if (string.IsNullOrEmpty(connectionString)) return;

        builder.Services.AddSingleton<ILoggger, LoggerService>();

        builder.Services.AddSingleton<IPasswordhasher, Passwordhasher>();



        builder.Services.AddTransient<IFragranceRepo>(sp =>
            new SqlFragranceRepo(connectionString, sp.GetRequiredService<IPasswordhasher>(),
            sp.GetRequiredService<ILoggger>()));

        builder.Services.AddTransient<IFragranceRepo>(sp =>
                new SqlFragranceRepo(connectionString, sp.GetRequiredService<IPasswordhasher>(),
                sp.GetRequiredService<ILoggger>()));
        builder.Services.AddTransient<IAdminServices>(sp =>
                   new AdminServices(connectionString,
                   sp.GetRequiredService<ILoggger>()));

       

        builder.Services.AddTransient<FragranceEngine>();

        builder.Logging.SetMinimumLevel(LogLevel.Warning);


        builder.Services.AddTransient<FragranceEngine>();

        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        using IHost host = builder.Build();

        var logic = host.Services.GetRequiredService<FragranceEngine>();
        await logic.RUN();


    }
}

