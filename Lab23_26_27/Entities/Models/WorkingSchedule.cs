namespace HospitalApi.Entities;

public class WorkingSchedule
{
    public int Id { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
     
    public string DayOfWeek { get; set; }

    public virtual ICollection<DoctorWorkingSchedule> DoctorWorkingSchedules { get; set; } = new List<DoctorWorkingSchedule>();
}
