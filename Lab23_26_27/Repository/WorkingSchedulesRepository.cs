using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class WorkingSchedulesRepository : RepositoryBase<WorkingSchedule>, IWorkingScheduleRepository
{
    public WorkingSchedulesRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(WorkingSchedule workingSchedule)
    {
        await AddAsync(workingSchedule); 
    }

    public void Remove(WorkingSchedule workingSchedule)
    {
        Delete(workingSchedule);
    }

    public async Task<IEnumerable<WorkingSchedule>> GetAllAsync(bool trackChanges)
    {
        var workingSchedule = await GetAll(trackChanges)
                                   .Select(ws => new WorkingSchedule
                                   {
                                       Id = ws.Id,
                                       StartTime = ws.StartTime,
                                       EndTime = ws.EndTime,
                                       DayOfWeek = ws.DayOfWeek,
                                   })
                                   .ToListAsync();

        return workingSchedule;
    }

    public async Task<WorkingSchedule> GetAsync(int id, bool trackChanges)
    {
        var workingSchedule = await Find(ws => ws.Id.Equals(id), trackChanges)
                                   .FirstOrDefaultAsync();

        return workingSchedule ?? throw new WorkingScheduleNotFoundException($"Working schedule with id:{id} not found");
    }
}
