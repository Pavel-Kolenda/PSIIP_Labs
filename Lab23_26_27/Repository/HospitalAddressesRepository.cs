using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class HospitalAddressesRepository : RepositoryBase<HospitalAddress>, IHospitalAddressesRepository
{
    public HospitalAddressesRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(int hospitalId, HospitalAddress hospitalAddress)
    {
        hospitalAddress.HospitalId = hospitalId;

        await AddAsync(hospitalAddress);
    }

    public void Remove(HospitalAddress hospitalAddress)
    {
        Delete(hospitalAddress);
    }

    public async Task<IEnumerable<HospitalAddress>> GetAllAsync(int hospitalId, bool trackChanges)
    {
        var hospitalAddresses = await Find(ha => ha.HospitalId.Equals(hospitalId), trackChanges)
                                     .Select(ha => new HospitalAddress
                                     {
                                         Id = ha.Id,
                                         City = ha.City,
                                         Address = ha.Address,
                                         Country = ha.Country
                                     })
                                     .ToListAsync();

        return hospitalAddresses;
    }

    public async Task<HospitalAddress> GetAsync(int hospitalId, int id, bool trackChanges)
    {
        var hospitalAddress = await Find(ha => ha.HospitalId.Equals(hospitalId) && ha.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

        return hospitalAddress ?? throw new HospitalAddressNotFoundException($"Hospital address not found");
    }

    public async Task<(int hospitalAddressId, int hospitalId)> GetAddressIdAndHospitalIds(string address, string city, string country)
    {
        var hospitalAddress = await _hospitalContext
            .HospitalAddresses
            .Where
            (
                h => h.Address.ToLower().Contains(address.ToLower()) &&
                h.City.ToLower() == city.ToLower() &&
                h.Country.ToLower() == country.ToLower()
            )
            .Select(h => new HospitalAddress
            {
                Id = h.Id,
                HospitalId = h.HospitalId
            })
            .FirstOrDefaultAsync();
        


        if (hospitalAddress == null)
            throw new HospitalAddressNotFoundException($"Hospital with address:{address} in city:{city} not found");

        return (hospitalAddress.Id, hospitalAddress.HospitalId);
    }
}
