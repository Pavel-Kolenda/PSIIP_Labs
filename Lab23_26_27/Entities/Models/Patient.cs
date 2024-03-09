namespace HospitalApi.Entities;

public class Patient
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber { get; set; }

    public int AddressId { get; set; }
    public virtual HospitalAddress Address { get; set; }

    public int HospitalId { get; set; }
    public Hospital Hospital { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<OutpatientRecord> OutpatientRecords { get; set; } = new List<OutpatientRecord>();
}
