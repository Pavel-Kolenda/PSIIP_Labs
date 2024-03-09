namespace Entities.Exceptions.Hospital;
public class HospitalDoesNotServeAddressException : Exception
{
    public HospitalDoesNotServeAddressException() : base("The hospital does not serve the provided address.")
    {
        
    }
}
