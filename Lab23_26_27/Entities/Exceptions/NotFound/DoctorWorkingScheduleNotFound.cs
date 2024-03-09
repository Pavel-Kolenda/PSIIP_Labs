namespace Entities.Exceptions.NotFound;
public class DoctorWorkingScheduleNotFound : NotFoundException
{
    public DoctorWorkingScheduleNotFound(string message) : base(message)
    {
    }
}
