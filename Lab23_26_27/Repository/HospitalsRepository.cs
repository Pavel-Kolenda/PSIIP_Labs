using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class HospitalsRepository : RepositoryBase<Hospital>, IHospitalsRepository
{
    public HospitalsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(Hospital hospital)
    {
        await AddAsync(hospital);
    }

    public void Remove(Hospital hospital)
    {
        Delete(hospital);
    }

    public async Task<IEnumerable<Hospital>> GetAllAsync(bool trackChanges)
    {
        var hospitals = await GetAll(trackChanges)
                             .Select(h => CreateHospital(h))
                             .ToListAsync();

        return hospitals;
    }

    public async Task<Hospital> GetAsync(int id, bool trackChanges)
    {
        var hospital = await Find(h => h.Id.Equals(id), trackChanges)
                            .Select(h => CreateHospital(h))
                            .FirstOrDefaultAsync();


        return hospital ?? throw new HospitalNotFoundException($"Hospital with Id:{id} not found");
    }

    private static Hospital CreateHospital(Hospital hospital)
    {
        return new Hospital
        {
            Id = hospital.Id,
            Address = hospital.Address,
            City = hospital.City,
            Country = hospital.Country
        };
    }
}
