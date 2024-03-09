using Contracts;
using HospitalApi.Configuration;
using HospitalApi.ExceptionHandling;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ExceptionLoggingHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandling>();

builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();

builder.Services.AddScoped<IDoctorsService, DoctorsService>();

builder.Services.AddScoped<IPatientsService, PatientsService>();

builder.Services.AddScoped<ISpecializationsService, SpecializationsService>();

builder.Services.AddScoped<IWorkingSchedulesService, WorkingSchedulesService>();

builder.Services.AddScoped<IDoctorWorkingSchedulesService, DoctorWorkingSchedulesService>();

builder.Services.AddScoped<IOutpatientRecordsService, OutpatientRecordsService>();

builder.Services.AddScoped<IHospitalsService, HospitalsService>();

builder.Services.AddScoped<IHospitalAddressesService, HospitalAddressesService>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddMvc()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(typeof(Program));

DatabaseConfiguration dbConfiguration = new();

builder.Configuration.GetSection("DatabaseConfig").Bind(dbConfiguration);


builder.Services.AddDbContextPool<HospitalContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        ServerVersion.Parse("8.0.34-mysql"),
        action =>
        {
            action.CommandTimeout(dbConfiguration.TimeoutTime);
        })
        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging(dbConfiguration.SensitiveDataLogging);

    options.EnableDetailedErrors(dbConfiguration.DetailedError);
});

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("X-Pagination"));
});

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
