using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class SpecializationsRepository : RepositoryBase<Specialization>, ISpecializationRepository
{
    public SpecializationsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(Specialization specialization)
    {
        await AddAsync(specialization);
    }

    public void Remove(Specialization specialization)
    {
        Delete(specialization);
    }

    public async Task<IEnumerable<Specialization>> GetAllAsync(bool trackChanges)
    {
        var specializations = await GetAll(trackChanges)
                                   .Select(s => new Specialization
                                   {
                                       Id = s.Id,
                                       SpecializationName = s.SpecializationName
                                   })
                                   .ToListAsync();

        return specializations; 
    }

    public async Task<Specialization> GetAsync(int id, bool trackChanges)
    {
        var specialization = await Find(spec => spec.Id.Equals(id), trackChanges)
                                  .FirstOrDefaultAsync();

        return specialization ?? throw new SpecializationNotFoundException($"Specialization with id:{id} not found");
    }

    public async Task<int> GetIdByNameAsync(string specializationName, bool trackChanges)
    {
        var specializationId = await Find(spec => spec.SpecializationName.Equals(specializationName), trackChanges)
                                    .Select(s => s.Id)
                                    .FirstOrDefaultAsync();

        if (specializationId == 0)
            throw new SpecializationNotFoundException($"Specialization with specialization name: {specializationName} not found");

        return specializationId;
    }
}
