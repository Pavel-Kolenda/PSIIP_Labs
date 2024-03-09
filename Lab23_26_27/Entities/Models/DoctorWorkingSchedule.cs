namespace HospitalApi.Entities;

public class DoctorWorkingSchedule
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int WorkingScheduleId { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual WorkingSchedule WorkingSchedule { get; set; }
}
