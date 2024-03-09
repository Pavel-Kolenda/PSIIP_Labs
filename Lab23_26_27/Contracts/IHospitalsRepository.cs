using HospitalApi.Entities;

namespace Contracts;
public interface IHospitalsRepository
{
    Task<IEnumerable<Hospital>> GetAllAsync(bool trackChanges);
    Task<Hospital> GetAsync(int id, bool trackChanges);
    Task CreateAsync(Hospital hospital);
    void Remove(Hospital hospital);
}
