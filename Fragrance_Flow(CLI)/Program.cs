using Fragrance_flow_DL_VERSION_.classes;
using Fragrance_flow_DL_VERSION_.classes.Fragrance_Engine;
using Fragrance_flow_DL_VERSION_.classes.logic.Suggestion_logic;
using Fragrance_flow_DL_VERSION_.classes.Services;
using Fragrance_flow_DL_VERSION_.classes.Sql;
using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
// TIME WASTED (Including writting the 1000 line prototype) : 69h 0m. GAWDDAMN
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

        builder.Services.AddTransient<ISuggestion>(sp =>
           new SuggestionLogic(connectionString, sp.GetRequiredService<ILoggger>()));

        builder.Services.AddTransient<SuggestionLogic>();
        // builder.Services.AddTransient<ICli, Clirepo>();

        builder.Services.AddTransient<FragranceEngine>();

        builder.Logging.SetMinimumLevel(LogLevel.Warning);



        // builder.Services.AddTransient<SuggestionLogic>();

        //builder.Services.AddTransient<ICli, Clirepo>();


        builder.Services.AddTransient<FragranceEngine>();

        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        using IHost host = builder.Build();

        var logic = host.Services.GetRequiredService<FragranceEngine>();
        await logic.RUN();


    }
}

