namespace HospitalApi.Entities;

public class OutpatientRecord
{
    public int Id { get; set; }

    public int DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }
     

    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public string Description { get; set; }

    public DateTime RecordDate { get; set; }
}
 