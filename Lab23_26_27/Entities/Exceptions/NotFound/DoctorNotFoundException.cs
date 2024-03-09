namespace Entities.Exceptions.NotFound;

public class DoctorNotFoundException : NotFoundException
{
    public DoctorNotFoundException(string message) : base(message)
    {
    }
}
