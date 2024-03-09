namespace Contracts;
public interface IRepositoryManager
{
    IAppointmentsRepository AppointmentsRepository { get; }
    IDoctorsRepository DoctorsRepository { get; }
    IPatientsRepository PatientsRepository { get; }
    ISpecializationRepository SpecializationsRepository { get; }
    IWorkingScheduleRepository WorkingSchedulesRepository { get; }
    IDoctorWorkingSchedulesRepository DoctorWorkingSchedulesRepository { get; }
    IOutpatientRecordsRepository OutpatientRecordsRepository { get; }
    IHospitalsRepository HospitalsRepository { get; }
    IHospitalAddressesRepository HospitalAddressesRepository { get; }
    Task SaveChangesAsync();
}
