using fragrance_API.dbcontext;
using Fragrance_flow_DL_VERSION_;
using Fragrance_flow_DL_VERSION_.classes;
using Fragrance_flow_DL_VERSION_.classes.Fragrance_Engine;
using Fragrance_flow_DL_VERSION_.classes.logic.Suggestion_logic;
using Fragrance_flow_DL_VERSION_.classes.Services;
using Fragrance_flow_DL_VERSION_.classes.Sql;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
if (connectionString == null) return;

builder.Services.AddSingleton<ILoggger, LoggerService>();

builder.Services.AddSingleton<IPasswordhasher, Passwordhasher>();

builder.Services.AddTransient<IFragranceRepo>(sp =>
    new SqlFragranceRepo(connectionString, sp.GetRequiredService<IPasswordhasher>(),
    sp.GetRequiredService<ILoggger>()));

builder.Services.AddTransient<IAdminServices>(sp =>
           new AdminServices(connectionString,
           sp.GetRequiredService<ILoggger>()));

builder.Services.AddSingleton<Dbcontext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddControllers();

var app = builder.Build();


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

