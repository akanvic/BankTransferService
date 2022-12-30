using BankTransferService.Core.Entities;
using BankTransferService.Repo.Dapper.Infrastructure;
using BankTransferService.Repo.Data;
using BankTransferService.Repo.Data.Repository.Implementations;
using BankTransferService.Repo.Data.Repository.Interfaces;
using BankTransferService.Service.Implementation;
using BankTransferService.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ReadConfig>(builder.Configuration.GetSection("ReadConfig"));
var config = builder.Configuration.GetSection("ReadConfig");
ReadConfig readconfig = config.Get<ReadConfig>();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(readconfig.DefaultConnection));

//JWT IMPLEMENTATION

var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(readconfig.Secret));
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = readconfig.Issuer,
        ValidAudiences = new List<string>
              {
                readconfig.Audience1
              },
        IssuerSigningKey = secretKey
    };
});

//SERVICES
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddSingleton<IConnectionFactory, Connectionfactory>();
builder.Services.AddScoped<IFlutterwaveGateway, FlutterwaveGateway>();
builder.Services.AddHttpClient();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(configuration => configuration
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .WithExposedHeaders("Content-Disposition"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
