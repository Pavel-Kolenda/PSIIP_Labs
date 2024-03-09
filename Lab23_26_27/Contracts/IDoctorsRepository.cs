using HospitalApi.Entities;
using Shared;

namespace Contracts;
public interface IDoctorsRepository
{
    IQueryable<Doctor> GetAllAsync(bool trackChanges);
    Task<Doctor> GetAsync(int id, bool trackChanges);
    Task CreateAsync(Doctor doctor, int specializationId);
    void Remove(Doctor doctor);

    Task<Doctor> GetWithHospitalAndSpecializationAsync(int hospitalId, int id, bool trackChanges);
    IEnumerable<Doctor> GetAllWithHospitalAndSpecialization(int hospitalId, bool trackChanges, RequestParameters requestParameters);
    Task<int> GetIdByFullNameAsync(string firstName, string surname);
}
