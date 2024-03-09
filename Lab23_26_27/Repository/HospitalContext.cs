using HospitalApi.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace HospitalApi.Repository;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorWorkingSchedule> DoctorWorkingSchedules { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<HospitalAddress> HospitalAddresses { get; set; }

    public virtual DbSet<OutpatientRecord> OutpatientRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<WorkingSchedule> WorkingSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

        modelBuilder.ApplyConfiguration(new DoctorConfiguration());

        modelBuilder.ApplyConfiguration(new DoctorWorkingScheduleConfiguration());

        modelBuilder.ApplyConfiguration(new HospitalConfiguration());

        modelBuilder.ApplyConfiguration(new HospitalAddressConfiguration());

        modelBuilder.ApplyConfiguration(new OutpatientRecordConfiguration());

        modelBuilder.ApplyConfiguration(new PatientConfiguration());

        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());

        modelBuilder.ApplyConfiguration(new WorkingScheduleConfiguration());
    }
}
