using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.Hospitals;

namespace Services;
public class HospitalsService : IHospitalsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public HospitalsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<HospitalDto> CreateAsync(HospitalForCreation hospitalForCreation)
    {
        var hospital = _mapper.Map<Hospital>(hospitalForCreation);

        await _repositoryManager.HospitalsRepository.CreateAsync(hospital);

        await _repositoryManager.SaveChangesAsync();

        HospitalDto hospitalToReturn = _mapper.Map<HospitalDto>(hospital);

        return hospitalToReturn;
    }

    public async Task DeleteAsync(int id, bool trackChanges)
    {
        var hospital = await _repositoryManager.HospitalsRepository.GetAsync(id, trackChanges);

        _repositoryManager.HospitalsRepository.Remove(hospital);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<HospitalDto> GetAsync(int id, bool trackChanges)
    {
        var hospital = await _repositoryManager.HospitalsRepository.GetAsync(id, trackChanges);

        var hospitalDto = _mapper.Map<HospitalDto>(hospital);

        return hospitalDto;
    }

    public async Task<IEnumerable<HospitalDto>> GetAllAsync(bool trackChanges)
    {
        var hospitals = await _repositoryManager.HospitalsRepository.GetAllAsync(trackChanges);

        var hospitalsDto = _mapper.Map<IEnumerable<HospitalDto>>(hospitals);

        return hospitalsDto;    
    }

    public async Task UpdateAsync(int id, HospitalForUpdate hospitalForUpdate, bool trackChanges)
    {
        var hospital = await _repositoryManager.HospitalsRepository.GetAsync(id, trackChanges);

        _mapper.Map(hospitalForUpdate, hospital);

        await _repositoryManager.SaveChangesAsync();
    }
}
