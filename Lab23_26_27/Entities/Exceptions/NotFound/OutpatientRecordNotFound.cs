namespace Entities.Exceptions.NotFound;
public class OutpatientRecordNotFound : NotFoundException
{
    public OutpatientRecordNotFound(string message) : base(message)
    {
    }
}
