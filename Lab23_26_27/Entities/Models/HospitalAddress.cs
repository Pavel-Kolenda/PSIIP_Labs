namespace HospitalApi.Entities;

public class HospitalAddress
{
    public int Id { get; set; }

    public int HospitalId { get; set; }

    public string City { get; set; } 

    public string Address { get; set; }

    public string Country { get; set; }

    public virtual Hospital Hospital { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
