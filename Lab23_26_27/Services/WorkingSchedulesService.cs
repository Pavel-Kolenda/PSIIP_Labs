using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.WorkingSchedules;

namespace Services;
public class WorkingSchedulesService : IWorkingSchedulesService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public WorkingSchedulesService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<WorkingScheduleDto> CreateAsync(WorkingScheduleForCreation workingScheduleForCreation)
    {
        ValidateWorkingTime(workingScheduleForCreation);

        var workingSchedule = _mapper.Map<WorkingSchedule>(workingScheduleForCreation);

        await _repositoryManager.WorkingSchedulesRepository.CreateAsync(workingSchedule);

        await _repositoryManager.SaveChangesAsync();

        WorkingScheduleDto workingScheduleToReturn = _mapper.Map<WorkingScheduleDto>(workingSchedule);

        return workingScheduleToReturn;
    }

    public async Task DeleteAsync(int id, bool trackChanges)
    {
        var workingSchedule = await _repositoryManager
            .WorkingSchedulesRepository
            .GetAsync(id, trackChanges);

        _repositoryManager.WorkingSchedulesRepository.Remove(workingSchedule);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<IEnumerable<WorkingScheduleDto>> GetAllAsync(bool trackChanges)
    {
        var workingSchedules = await _repositoryManager.WorkingSchedulesRepository.GetAllAsync(trackChanges);

        var workingSchedulesDto = _mapper.Map<IEnumerable<WorkingScheduleDto>>(workingSchedules);

        return workingSchedulesDto;
    }

    public async Task<WorkingScheduleDto> GetAsync(int id, bool trackChanges)
    {
        var workingSchedule = await _repositoryManager.WorkingSchedulesRepository.GetAsync(id, trackChanges);

        var workingScheduleDto = _mapper.Map<WorkingScheduleDto>(workingSchedule);

        return workingScheduleDto;
    }

    public async Task UpdateAsync(int id, WorkingScheduleForUpdate workingScheduleForUpdate, bool trackChanges)
    {
        ValidateWorkingTime(workingScheduleForUpdate);

        var workingSchedule = await _repositoryManager.WorkingSchedulesRepository.GetAsync(id, trackChanges);

        _mapper.Map(workingScheduleForUpdate, workingSchedule);

        await _repositoryManager.SaveChangesAsync();
    }
    private void ValidateWorkingTime(WorkingScheduleForManipulation workingSchedule)
    {
        if (workingSchedule.StartTime > workingSchedule.EndTime)
            throw new ArgumentException("Start time cannot be later than end time.");

        TimeSpan difference = workingSchedule.EndTime - workingSchedule.StartTime;

        if (difference.TotalHours > 12)
            throw new ArgumentException("Working shift cannot exceed 12 hours.");
    }

}
