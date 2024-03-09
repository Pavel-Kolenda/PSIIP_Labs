namespace HospitalApi.Entities;

public partial class Hospital
{
    public int Id { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<HospitalAddress> HospitalAddresses { get; set; } = new List<HospitalAddress>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
 