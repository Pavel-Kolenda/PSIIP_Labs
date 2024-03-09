using HospitalApi.Entities;

namespace Contracts;
public interface IDoctorWorkingSchedulesRepository
{
    Task<DoctorWorkingSchedule> GetByIdAsync(int doctorId, int dwsId, bool trackChanges);
    Task CreateAsync(int doctorId, DoctorWorkingSchedule doctorWorkingSchedule);
    void Remove(DoctorWorkingSchedule doctorWorkingSchedule);
     
    Task<IEnumerable<DoctorWorkingSchedule>> GetForDoctorAsync(int hospitalId, int doctorId, bool trackChanges);
    IEnumerable<DoctorWorkingSchedule> GetAllWithDoctors(int hospitalId, bool trackChanges);
}