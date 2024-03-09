namespace Entities.Exceptions.NotFound;
public class WorkingScheduleNotFoundException : NotFoundException
{
    public WorkingScheduleNotFoundException(string message) : base(message)
    {
    }
}
