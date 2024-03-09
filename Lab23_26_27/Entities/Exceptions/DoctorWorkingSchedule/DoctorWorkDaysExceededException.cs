namespace Entities.Exceptions.DoctorWorkingSchedule;
public class DoctorWorkDaysExceededException : BusinessRuleViolationException
{
    public DoctorWorkDaysExceededException(string message) : base(message)
    {
    }
}
