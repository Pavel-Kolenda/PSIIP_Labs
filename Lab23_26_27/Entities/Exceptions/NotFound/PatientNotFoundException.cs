namespace Entities.Exceptions.NotFound;
public class PatientNotFoundException : NotFoundException
{
    public PatientNotFoundException(string message) : base(message)
    {
    }
}

