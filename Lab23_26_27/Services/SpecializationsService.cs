using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.Specializations;

namespace Services;
public class SpecializationsService : ISpecializationsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public SpecializationsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<SpecializationDto> CreateAsync(SpecializationForCreation specializationForCreation)
    {
        var specialization = _mapper.Map<Specialization>(specializationForCreation);

        await _repositoryManager.SpecializationsRepository.CreateAsync(specialization);

        await _repositoryManager.SaveChangesAsync();

        SpecializationDto specializationToReturn = _mapper.Map<SpecializationDto>(specialization);

        return specializationToReturn;
    }

    public async Task DeleteAsync(int id, bool trackChanges)
    {
        var specialization = await _repositoryManager.SpecializationsRepository.GetAsync(id, trackChanges);

        _repositoryManager.SpecializationsRepository.Remove(specialization);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<IEnumerable<SpecializationDto>> GetAllAsync(bool trackChanges)
    {
        var specializations = await _repositoryManager.SpecializationsRepository.GetAllAsync(trackChanges);

        var specializationsDto = _mapper.Map<IEnumerable<SpecializationDto>>(specializations);

        return specializationsDto;
    }

    public async Task<SpecializationDto> GetAsync(int id, bool trackChanges)
    {
        var specialization = await _repositoryManager.SpecializationsRepository.GetAsync(id, trackChanges);

        var specializationsDto = _mapper.Map<SpecializationDto>(specialization);

        return specializationsDto;
    }

    public async Task UpdateAsync(int id, SpecializationForUpdate specializationForUpdate, bool trackChanges)
    {
        var specialization = await _repositoryManager.SpecializationsRepository.GetAsync(id, trackChanges);

        _mapper.Map(specializationForUpdate, specialization);

        await _repositoryManager.SaveChangesAsync();
    }
}
