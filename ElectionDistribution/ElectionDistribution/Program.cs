using AutoMapper;
using ElectionDistribution.RepositoryLayer;
using ElectionDistribution.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependancy Injection

builder.Services.AddScoped<IUserRegistrationSL, UserRegistrationSL>();
builder.Services.AddScoped<IUserRegistrationRepo, UserRegistrationRepo>();
builder.Services.AddScoped<IRevenueSubDivisionSL, RevenueSubDivisionSL>();
builder.Services.AddScoped<IRevenueSubDivisionRepo, RevenueSubDivisionRepo>();
builder.Services.AddScoped<IVoterSL, VoterSL>();
builder.Services.AddScoped<IVoterRepo, VoterRepo>();
builder.Services.AddAutoMapper(typeof(Program));
#endregion
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
