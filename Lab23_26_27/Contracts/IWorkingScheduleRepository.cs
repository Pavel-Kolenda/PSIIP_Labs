using HospitalApi.Entities;

namespace Contracts;
public interface IWorkingScheduleRepository
{
    Task<IEnumerable<WorkingSchedule>> GetAllAsync(bool trackChanges);
    Task<WorkingSchedule> GetAsync(int id, bool trackChanges);
    Task CreateAsync(WorkingSchedule workingSchedule);
    void Remove(WorkingSchedule workingSchedule);
}
