namespace Entities.Exceptions.NotFound;
public class HospitalAddressNotFoundException : NotFoundException
{
    public HospitalAddressNotFoundException(string message) : base(message)
    {
    }
}
