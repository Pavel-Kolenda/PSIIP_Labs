namespace HospitalApi.Entities;

public class Appointment
{
    public int Id { get; set; }

    public int HospitalId { get; set; }
    public virtual Hospital Hospital { get; set; }

    public int DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }

    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public DateTime AppointmentTime { get; set; }

}
