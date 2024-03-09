using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.OutpatientRecords;

namespace Services;
public class OutpatientRecordsService : IOutpatientRecordsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public OutpatientRecordsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<OutpatientRecordDto> CreateAsync(int patientId, OutpatientRecordForCreation outpatientRecordForCreation)
    {
        var outpatientRecord = _mapper.Map<OutpatientRecord>(outpatientRecordForCreation);

        await _repositoryManager.OutpatientRecordsRepository.CreateAsync(patientId, outpatientRecord);

        await _repositoryManager.SaveChangesAsync();

        OutpatientRecordDto outpatientRecordToReturn = _mapper.Map<OutpatientRecordDto>(outpatientRecord);

        return outpatientRecordToReturn;
    }

    public async Task<OutpatientRecordDto> GetWithDoctorAsync(int patientId, int outpatientRecordId, bool trackChanges)
    {
        var outpatientRecord = await _repositoryManager
                                    .OutpatientRecordsRepository
                                    .GetWithDoctorAsync(patientId, outpatientRecordId, trackChanges);


        var outpatientRecordDto = _mapper.Map<OutpatientRecordDto>(outpatientRecord);

        return outpatientRecordDto;
    }
    public async Task<IEnumerable<OutpatientRecordDto>> GetWithDoctorAsync(int patientId, bool trackChanges)
    {
        var outpatientRecords = await _repositoryManager
            .OutpatientRecordsRepository
            .GetAllWithDoctorAsync(patientId, trackChanges);

        var outpatientRecordsDto = _mapper.Map<IEnumerable<OutpatientRecordDto>>(outpatientRecords);

        return outpatientRecordsDto;
    }

    public async Task DeleteAsync(int patientId, int outpatientRecordId, bool trackChanges)
    {
        var outpatientRecord = await _repositoryManager
            .OutpatientRecordsRepository
            .GetAsync(patientId, outpatientRecordId, trackChanges);

        _repositoryManager.OutpatientRecordsRepository.Remove(outpatientRecord);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task UpdateAsync(int patientId,
        int outpatientRecordId,
        OutpatientRecordForUpdate outpatientRecordForUpdate,
        bool trackChanges)
    {
        var outpatientRecord = await _repositoryManager
            .OutpatientRecordsRepository
            .GetAsync(patientId, outpatientRecordId, trackChanges);

        _mapper.Map(outpatientRecordForUpdate, outpatientRecord);

        await _repositoryManager.SaveChangesAsync();
    }
}
