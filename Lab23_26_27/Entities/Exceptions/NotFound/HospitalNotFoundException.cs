namespace Entities.Exceptions.NotFound;

public class HospitalNotFoundException : NotFoundException
{
    public HospitalNotFoundException(string message) : base(message)
    {
    }
}
