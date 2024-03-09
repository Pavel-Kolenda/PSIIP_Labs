namespace Entities.Exceptions.Patient;
public class PatientDoesNotLiveOnAddressException : Exception
{
    public PatientDoesNotLiveOnAddressException() : base("Patient with provided data doesn't live on this address") { }
}
