using BankTransferService.Core.Entities;
using BankTransferService.Repo.Data;
using BankTransferService.Repo.Data.Repository.Implementations;
using BankTransferService.Repo.Data.Repository.Interfaces;
using BankTransferService.Service.Implementation;
using BankTransferService.Service.Interface;
using Microsoft.EntityFrameworkCore;
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


builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(readconfig.DefaultConnection));

//SERVICES
builder.Services.AddScoped<IPaystackGateway, PaystackGateway>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
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
