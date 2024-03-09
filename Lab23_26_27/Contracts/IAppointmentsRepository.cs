using HospitalApi.Entities;
using Shared;

namespace Contracts;
public interface IAppointmentsRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync(bool trackChanges);
    Task<Appointment> GetAsync(int id, bool trackChanges);
    Task CreateAsync(Appointment appointment);
    void Remove(Appointment appointment);
    Task<Appointment> GetWithDoctorAndPatientAsync(int hospitalId, int id, bool trackChanges);

    PagedList<Appointment> GetAllWithDoctorAndPatientAsync(int hospitalId, bool trackChanges, RequestParameters requestParameters);
}

