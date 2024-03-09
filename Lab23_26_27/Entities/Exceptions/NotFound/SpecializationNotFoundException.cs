namespace Entities.Exceptions.NotFound;

public class SpecializationNotFoundException : NotFoundException
{
    public SpecializationNotFoundException(string message) : base(message)
    {
    }
}