using HospitalApi.Entities;

namespace Contracts;
public interface IPatientsRepository
{
    Task<IEnumerable<Patient>> GetAllAsync(int hospitalId, bool trackChanges);
    Task<Patient> GetAsync(int hospitalId, int id, bool trackChanges);
    Task CreateAsync(Patient patient, int addressId, int hospitalId);
    void Remove(Patient patient);
    Task<Patient> GetWithAddressAsync(int hospitalId, int id, bool trackChanges);
    IQueryable<Patient> GetAllWithAddressAsync(int hospitalId, bool trackChanges);

    Task<int> GetIdByFullNameAsync(string name, string surname);
}
