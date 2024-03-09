using Contracts;
using HospitalApi.Repository;

namespace Repository;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly HospitalContext _hospitalContext;
    private readonly Lazy<IAppointmentsRepository> _appointmentRepository;
    private readonly Lazy<IDoctorsRepository> _doctorRepository;
    private readonly Lazy<IPatientsRepository> _patientRepository;
    private readonly Lazy<ISpecializationRepository> _specializationRepository;
    private readonly Lazy<IWorkingScheduleRepository> _workingScheduleRepository;
    private readonly Lazy<IDoctorWorkingSchedulesRepository> _doctorWorkingSchedule;
    private readonly Lazy<IOutpatientRecordsRepository> _outpatientRecordRepository;
    private readonly Lazy<IHospitalsRepository> _hospitalRepository;
    private readonly Lazy<IHospitalAddressesRepository> _hospitalAddressRepository;
    public RepositoryManager(HospitalContext hospitalContext)
    {
        _hospitalContext = hospitalContext;
        _appointmentRepository = new Lazy<IAppointmentsRepository>(() => new AppointmentsRepository(_hospitalContext));
        _doctorRepository = new Lazy<IDoctorsRepository>(() => new DoctorsRepository(_hospitalContext));
        _patientRepository = new Lazy<IPatientsRepository>(() => new PatientsRepository(_hospitalContext));
        _specializationRepository = new Lazy<ISpecializationRepository>(() => new SpecializationsRepository(_hospitalContext));
        _workingScheduleRepository = new Lazy<IWorkingScheduleRepository>(() => new WorkingSchedulesRepository(_hospitalContext));
        _doctorWorkingSchedule = new Lazy<IDoctorWorkingSchedulesRepository>(() => new DoctorWorkingSchedulesRepository(_hospitalContext));
        _outpatientRecordRepository = new Lazy<IOutpatientRecordsRepository>(() => new OutpatientRecordsRepository(_hospitalContext));
        _hospitalRepository = new Lazy<IHospitalsRepository>(() => new HospitalsRepository(_hospitalContext));
        _hospitalAddressRepository = new Lazy<IHospitalAddressesRepository>(() => new HospitalAddressesRepository(_hospitalContext));
    }

    public IAppointmentsRepository AppointmentsRepository => _appointmentRepository.Value;
    public IDoctorsRepository DoctorsRepository => _doctorRepository.Value;   
    public IPatientsRepository PatientsRepository => _patientRepository.Value;   
    public ISpecializationRepository SpecializationsRepository => _specializationRepository.Value; 
    public IWorkingScheduleRepository WorkingSchedulesRepository => _workingScheduleRepository.Value;
    public IDoctorWorkingSchedulesRepository DoctorWorkingSchedulesRepository => _doctorWorkingSchedule.Value;
    public IOutpatientRecordsRepository OutpatientRecordsRepository => _outpatientRecordRepository.Value;
    public IHospitalsRepository HospitalsRepository => _hospitalRepository.Value;
    public IHospitalAddressesRepository HospitalAddressesRepository => _hospitalAddressRepository.Value;

    public async Task SaveChangesAsync()
    {
        await _hospitalContext.SaveChangesAsync();
    }
}
