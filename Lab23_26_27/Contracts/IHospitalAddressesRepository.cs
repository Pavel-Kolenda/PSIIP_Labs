using HospitalApi.Entities;

namespace Contracts;
public interface IHospitalAddressesRepository
{
    Task<IEnumerable<HospitalAddress>> GetAllAsync(int hospitalId, bool trackChanges);
    Task<HospitalAddress> GetAsync(int hospitalId, int id, bool trackChanges);
    Task CreateAsync(int hospitalId, HospitalAddress hospitalAddress);
    void Remove(HospitalAddress hospitalAddress);
    Task<(int hospitalAddressId, int hospitalId)> GetAddressIdAndHospitalIds(string address, string city, string country);
}
