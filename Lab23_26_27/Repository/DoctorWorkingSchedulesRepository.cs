using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class DoctorWorkingSchedulesRepository : RepositoryBase<DoctorWorkingSchedule>, IDoctorWorkingSchedulesRepository
{
    public DoctorWorkingSchedulesRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(int doctorId, DoctorWorkingSchedule doctorWorkingSchedule)
    {
        doctorWorkingSchedule.DoctorId = doctorId;
        await AddAsync(doctorWorkingSchedule);
    }

    public void Remove(DoctorWorkingSchedule doctorWorkingSchedule)
    {
        Delete(doctorWorkingSchedule);
    }

    public async Task<DoctorWorkingSchedule> GetByIdAsync(int doctorId, int dwsId, bool trackChanges)
    {
        var workingSchedule = await Find(ws => ws.DoctorId.Equals(doctorId) && ws.Id.Equals(dwsId), trackChanges)
                                   .FirstOrDefaultAsync();

        return workingSchedule ?? throw new DoctorWorkingScheduleNotFound($"Doctor working schedule not found");
    }

    public async Task<IEnumerable<DoctorWorkingSchedule>> GetForDoctorAsync(int hospitalId, int doctorId, bool trackChanges)
    {
        var dws = await Find(d => d.Doctor.Id.Equals(doctorId) && d.Doctor.HospitalId.Equals(hospitalId), trackChanges)
                       .Include(d => d.Doctor)
                       .Include(ws => ws.WorkingSchedule)
                       .Select(dws => CreateDoctorWorkingSchedule(dws))
                       .ToListAsync();

        return dws;
    }

    public IEnumerable<DoctorWorkingSchedule> GetAllWithDoctors(int hospitalId, bool trackChanges)
    {
        var dws = GetAll(trackChanges)
            .Include(dws => dws.Doctor)
            .Include(dws => dws.WorkingSchedule)
            .Where(dws => dws.Doctor.HospitalId == hospitalId)
            .Select(dws => CreateDoctorWorkingSchedule(dws));

        return dws;
    }

    private static DoctorWorkingSchedule CreateDoctorWorkingSchedule(DoctorWorkingSchedule dws)
    {
        return new DoctorWorkingSchedule
        {
            Id = dws.Id,
            Doctor = new Doctor
            {
                Name = dws.Doctor.Name,
                Surname = dws.Doctor.Surname
            },
            WorkingSchedule = new WorkingSchedule
            {
                Id = dws.WorkingSchedule.Id,
                StartTime = dws.WorkingSchedule.StartTime,
                EndTime = dws.WorkingSchedule.EndTime,
                DayOfWeek = dws.WorkingSchedule.DayOfWeek
            }
        };
    }
}
