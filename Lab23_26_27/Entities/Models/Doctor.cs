namespace HospitalApi.Entities;

public class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
     
    public string Patronymic { get; set; }

    public int SpecializationId { get; set; }
    public virtual Specialization Specialization { get; set; }

    public int HospitalId { get; set; }
    public virtual Hospital Hospital { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<DoctorWorkingSchedule> DoctorWorkingSchedules { get; set; } = new List<DoctorWorkingSchedule>();

    public virtual ICollection<OutpatientRecord> OutpatientRecords { get; set; } = new List<OutpatientRecord>();
}
