using Azure.Core;
using fragrance_API.Controllers.AdminControllers;
using fragrance_API.Controllers.Fragrance_Controllers;
using fragrance_API.dbcontext;
using fragrance_API.jwt;
using Fragrance_flow_DL_VERSION_.classes;
using Fragrance_flow_DL_VERSION_.classes.Services;
using Fragrance_flow_DL_VERSION_.classes.Sql;
using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
?? throw new InvalidOperationException("'DB_CONNECTION' not found");

builder.Services.AddSingleton<ILoggger, LoggerService>();

builder.Services.AddSingleton<IPasswordhasher, Passwordhasher>();

builder.Services.AddTransient<IFragranceRepo>(sp =>
    new SqlFragranceRepo(connectionString, sp.GetRequiredService<IPasswordhasher>(),
    sp.GetRequiredService<ILoggger>()));

builder.Services.AddTransient<IAdminServices>(sp =>
           new AdminServices(connectionString,
           sp.GetRequiredService<ILoggger>()));

//builder.Services.AddTransient<GetAllUserInfoById>();

builder.Services.AddTransient<GetFragrancesForUserId>();

builder.Services.AddSingleton<TokenGenerator>();
builder.Services.AddSingleton<Dbcontext>();
builder.Services.AddTransient<CreateUser>();
builder.Services.AddTransient<AddFragranceController>();
builder.Services.AddTransient<GetAdminStatusController>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

