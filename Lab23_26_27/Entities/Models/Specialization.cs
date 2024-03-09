namespace HospitalApi.Entities;

public class Specialization
{
    public int Id { get; set; }

    public string SpecializationName { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
