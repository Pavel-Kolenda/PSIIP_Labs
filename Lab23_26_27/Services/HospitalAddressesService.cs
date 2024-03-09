using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.HospitalAddresses;

namespace Services;
public class HospitalAddressesService : IHospitalAddressesService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public HospitalAddressesService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<HospitalAddressDto> CreateAsync(int hospitalId, HospitalAddressForCreation hospitalAddressForCreation)
    {
        var hospitalAddress = _mapper.Map<HospitalAddress>(hospitalAddressForCreation);

        await _repositoryManager.HospitalAddressesRepository.CreateAsync(hospitalId, hospitalAddress);

        await _repositoryManager.SaveChangesAsync();

        HospitalAddressDto hospitalAddressToReturn = _mapper.Map<HospitalAddressDto>(hospitalAddress);

        return hospitalAddressToReturn;
    }

    public async Task DeleteAsync(int hospitalId, int id, bool trackChanges)
    {
        var hospitalAddress = await _repositoryManager.HospitalAddressesRepository.GetAsync(hospitalId, id, trackChanges);

        _repositoryManager.HospitalAddressesRepository.Remove(hospitalAddress);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<IEnumerable<HospitalAddressDto>> GetAllAsync(int hospitalId, bool trackChanges)
    {
        var hospitalAddresses = await _repositoryManager
            .HospitalAddressesRepository
            .GetAllAsync(hospitalId, trackChanges);

        var hospitalAddressesDto = _mapper.Map<IEnumerable<HospitalAddressDto>>(hospitalAddresses);

        return hospitalAddressesDto;
    }

    public async Task<HospitalAddressDto> GetAsync(int hospitalId, int id, bool trackChanges)
    {
        var hospital = await _repositoryManager.HospitalAddressesRepository.GetAsync(hospitalId, id, trackChanges);

        var hospitalDto = _mapper.Map<HospitalAddressDto>(hospital);

        return hospitalDto;
    }

    public async Task UpdateAsync(int hospitalId, int id, HospitalAddressForUpdate hospitalAddressForUpdate, bool trackChanges)
    {
        var hospitalAddress = await _repositoryManager.HospitalAddressesRepository.GetAsync(hospitalId, id, trackChanges);

        _mapper.Map(hospitalAddressForUpdate, hospitalAddress);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<bool> HospitalServesAddress(int hospitalId, string address)
    {
        var servedAddresses = await _repositoryManager
                            .HospitalAddressesRepository
                            .GetAllAsync(hospitalId, false);

        if (!servedAddresses.Any(ha => ha.Address!.Contains(address)))
            return false;

        return true;
    }
}
