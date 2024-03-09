using HospitalApi.Entities;

namespace Contracts;
public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync(bool trackChanges);
    Task<Specialization> GetAsync(int id, bool trackChanges);
    Task CreateAsync(Specialization specialization);
    void Remove(Specialization specialization);

    Task<int> GetIdByNameAsync(string specializationName, bool trackChanges);
}
