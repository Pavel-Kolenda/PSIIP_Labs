namespace Entities.Exceptions.NotFound;
public class AppointmentNotFoundException : NotFoundException
{
    public AppointmentNotFoundException(string message) : base(message)
    {
    }
}
