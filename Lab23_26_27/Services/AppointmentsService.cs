using AutoMapper;
using Contracts;
using Entities.Exceptions.Hospital;
using Entities.Exceptions.Patient;
using HospitalApi.Entities;
using Service.Contracts;
using Services.Contracts;
using Shared;
using Shared.Dtos.Appointments;

namespace Services;
public class AppointmentsService : IAppointmentsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IDoctorsService _doctorService;
    private readonly IHospitalAddressesService _hospitalAddressService;
    private readonly IPatientsService _patientService;
    private readonly IMapper _mapper;
    public AppointmentsService(IRepositoryManager repositoryManager,
                              IMapper mapper,
                              IDoctorsService doctorService,
                              IHospitalAddressesService hospitalAddressService,
                              IPatientsService patientService)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _doctorService = doctorService;
        _hospitalAddressService = hospitalAddressService;
        _patientService = patientService;
    }
    public async Task<AppointmentDto> CreateAsync(AppointmentForCreation appointmentForCreation)
    {
        var (hospital, doctorId, patientId) = await ValidateAndRetrieveAppointmentInfo(appointmentForCreation, true);

        var appointment = new Appointment()
        {
            HospitalId = hospital.Id,
            PatientId = patientId,
            DoctorId = doctorId,
            AppointmentTime = appointmentForCreation.AppointmentTime,
        };

        await _repositoryManager.AppointmentsRepository.CreateAsync(appointment);

        await _repositoryManager.SaveChangesAsync();

        AppointmentDto appointmentToReturn = _mapper.Map<AppointmentDto>(appointment); 

        return appointmentToReturn;
    }

    public async Task DeleteAsync(int id, bool trackChanges)
    {
        var appointment = await _repositoryManager.AppointmentsRepository.GetAsync(id, trackChanges);

        _repositoryManager.AppointmentsRepository.Remove(appointment);

        await _repositoryManager.SaveChangesAsync();
    }

    public IEnumerable<AppointmentDto> GetWithDoctorAndPatientAsync(int hospitalId,
        bool trackChanges,
        RequestParameters requestParameters)
    {
        var appointments = _repositoryManager
            .AppointmentsRepository
            .GetAllWithDoctorAndPatientAsync(hospitalId, trackChanges, requestParameters);

        var appointmentDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

        return appointmentDto;
    }

    public async Task<AppointmentDto> GetWithDoctorAndPatientAsync(int hospitalId, int id, bool trackChanges)
    {
        var appointment = await _repositoryManager
            .AppointmentsRepository
            .GetWithDoctorAndPatientAsync(hospitalId, id, trackChanges);

        var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

        return appointmentDto;
    }

    public async Task UpdateAsync(int id, AppointmentForUpdate appointmentForUpdate, bool trackChanges)
    {
        var appointment = await _repositoryManager.AppointmentsRepository.GetAsync(id, trackChanges);

        var (hospital, doctorId, patientId) = await ValidateAndRetrieveAppointmentInfo(appointmentForUpdate, trackChanges);


        appointment.DoctorId = doctorId;
        appointment.PatientId = patientId;
        appointment.HospitalId = hospital.Id;
        appointment.AppointmentTime = appointmentForUpdate.AppointmentTime;


        await _repositoryManager.SaveChangesAsync();
    }


    private async Task<(Hospital hospital, int doctorId, int patientId)> ValidateAndRetrieveAppointmentInfo
        (AppointmentForCreation appointment, bool trackChanges)
    {
        var hospital = await _repositoryManager
            .HospitalsRepository
            .GetAsync(appointment.HospitalNumber, false);

        var specializationId = await _repositoryManager
            .SpecializationsRepository
            .GetIdByNameAsync(appointment.AppointmentToSpecialization, trackChanges);

        var doctorId = _doctorService.GetIdByHospitalAndSpecialization(hospital.Id, specializationId);


        if (!await _hospitalAddressService.HospitalServesAddress(hospital.Id, appointment.PatientAddress))
        {
            throw new HospitalDoesNotServeAddressException();
        }


        if (!_patientService.DoesLiveOnAddress(hospital.Id, appointment.PatientAddress, appointment.PatientName, appointment.PatientSurname))
        {
            throw new PatientDoesNotLiveOnAddressException();
        }

        var patientId = await _repositoryManager
            .PatientsRepository
            .GetIdByFullNameAsync(appointment.PatientName, appointment.PatientSurname);

        return (hospital, doctorId, patientId);
    }
}
