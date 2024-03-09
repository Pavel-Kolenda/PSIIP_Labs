using AutoMapper;
using HospitalApi.Entities;
using Shared.Dtos.Appointments;
using Shared.Dtos.Doctors;
using Shared.Dtos.DoctorWorkingSchedules;
using Shared.Dtos.HospitalAddresses;
using Shared.Dtos.Hospitals;
using Shared.Dtos.OutpatientRecords;
using Shared.Dtos.Patients;
using Shared.Dtos.Specializations;
using Shared.Dtos.WorkingSchedules;

namespace HospitalApi.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Appointment
        CreateMap<Appointment, AppointmentDto>()
            .ForPath(dest => dest.DoctorName, opt => opt.MapFrom(x => x.Doctor.Name))
            .ForPath(dest => dest.DoctorSurname, opt => opt.MapFrom(x => x.Doctor.Surname))
            .ForPath(dest => dest.DoctorPatronymic, opt => opt.MapFrom(x => x.Doctor.Patronymic))
            .ForPath(dest => dest.PatientName, opt => opt.MapFrom(x => x.Patient.Name))
            .ForPath(dest => dest.PatientSurname, opt => opt.MapFrom(x => x.Patient.Surname))
            .ForPath(dest => dest.PatientPatronymic, opt => opt.MapFrom(x => x.Patient.Patronymic))
            .ForPath(dest => dest.HospitalNumber, opt => opt.MapFrom(x => x.Hospital.Id));

        CreateMap<AppointmentForUpdate, Appointment>()
            .ForPath(dest => dest.Doctor, opt => opt.Ignore())
            .ForPath(dest => dest.Patient, opt => opt.Ignore());
        #endregion

        #region Patient

        CreateMap<Patient, PatientDto>()
            .ForPath(dest => dest.City, opt => opt.MapFrom(x => x.Address.City))
            .ForPath(dest => dest.Country, opt => opt.MapFrom(x => x.Address.Country))
            .ForPath(dest => dest.Address, opt => opt.MapFrom(x => x.Address.Address));

        CreateMap<PatientForUpdate, Patient>()
            .ForPath(dest => dest.Appointments, opt => opt.Ignore())
            .ForPath(dest => dest.OutpatientRecords, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore())
            .ForPath(dest => dest.Address, opt => opt.Ignore());

        CreateMap<PatientForCreation, Patient>()
            .ForPath(dest => dest.Address, opt => opt.Ignore())
            .ForPath(dest => dest.Appointments, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore())
            .ForPath(dest => dest.OutpatientRecords, opt => opt.Ignore());

        #endregion

        #region Doctor
        CreateMap<Doctor, DoctorDto>()
            .ForPath(dest => dest.Specialization,
                opt => opt.MapFrom(x => x.Specialization.SpecializationName))
            .ForPath(dest => dest.Hospital,
                opt => opt.MapFrom(x => x.Hospital.Id));

        CreateMap<DoctorForCreation, Doctor>()
            .ForPath(dest => dest.HospitalId, opt => opt.MapFrom(x => x.Hospital))
            .ForPath(dest => dest.SpecializationId, opt => opt.Ignore())
            .ForPath(dest => dest.Specialization, opt => opt.Ignore())
            .ForPath(dest => dest.DoctorWorkingSchedules, opt => opt.Ignore())
            .ForPath(dest => dest.OutpatientRecords, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore())
            .ForPath(dest => dest.Appointments, opt => opt.Ignore());

        CreateMap<DoctorForUpdate, Doctor>()
            .ForPath(dest => dest.HospitalId, opt => opt.MapFrom(x => x.Hospital))
            .ForPath(dest => dest.SpecializationId, opt => opt.Ignore())
            .ForPath(dest => dest.Specialization, opt => opt.Ignore())
            .ForPath(dest => dest.DoctorWorkingSchedules, opt => opt.Ignore())
            .ForPath(dest => dest.OutpatientRecords, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore())
            .ForPath(dest => dest.Appointments, opt => opt.Ignore());

        #endregion

        #region Specialization
        CreateMap<Specialization, SpecializationDto>();

        CreateMap<SpecializationForCreation, Specialization>();

        CreateMap<SpecializationForUpdate, Specialization>();
        #endregion

        #region WorkingSchedule

        CreateMap<WorkingSchedule, WorkingScheduleDto>();

        CreateMap<WorkingScheduleForCreation, WorkingSchedule>()
            .ForPath(dest => dest.DoctorWorkingSchedules, opt => opt.Ignore());

        CreateMap<WorkingScheduleForUpdate, WorkingSchedule>()
            .ForPath(dest => dest.DoctorWorkingSchedules, opt => opt.Ignore());

        #endregion

        #region OutPatientRecord

        CreateMap<OutpatientRecord, OutpatientRecordDto>()
            .ForPath(dest => dest.DoctorFullName, opt => opt.MapFrom(x => $"{x.Doctor.Name} {x.Doctor.Surname}"))
            .ForPath(dest => dest.PatientFullName, opt => opt.MapFrom(x => $"{x.Patient.Name} {x.Patient.Surname}"));


        CreateMap<OutpatientRecordForUpdate, OutpatientRecord>()
            .ForPath(dest => dest.Doctor, opt => opt.Ignore())
            .ForPath(dest => dest.Patient, opt => opt.Ignore());

        CreateMap<OutpatientRecordForCreation, OutpatientRecord>()
            .ForPath(dest => dest.Doctor, opt => opt.Ignore())
            .ForPath(dest => dest.Patient, opt => opt.Ignore());

        #endregion

        #region Hospital

        CreateMap<Hospital, HospitalDto>()
            .ForPath(dest => dest.Id, opt => opt.MapFrom(x => x.Id));

        CreateMap<HospitalForCreation, Hospital>()
            .ForPath(dest => dest.HospitalAddresses, opt => opt.Ignore())
            .ForPath(dest => dest.Country, opt => opt.MapFrom(x => x.Country))
            .ForPath(dest => dest.Doctors, opt => opt.Ignore());

        CreateMap<HospitalForUpdate, Hospital>()
            .ForPath(dest => dest.HospitalAddresses, opt => opt.Ignore())
            .ForPath(dest => dest.Doctors, opt => opt.Ignore());

        #endregion

        #region HospitalAddress

        CreateMap<HospitalAddress, HospitalAddressDto>();

        CreateMap<HospitalAddressForCreation, HospitalAddress>()
            .ForPath(dest => dest.Patients, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore());

        CreateMap<HospitalAddressForUpdate, HospitalAddress>()
            .ForPath(dest => dest.Patients, opt => opt.Ignore())
            .ForPath(dest => dest.Hospital, opt => opt.Ignore())
            .ForPath(dest => dest.HospitalId, opt => opt.Ignore())
            .ForPath(dest => dest.Address, opt => opt.MapFrom(x => x.Address))
            .ForPath(dest => dest.City, opt => opt.MapFrom(x => x.City));

        #endregion

        #region DoctorWorkingSchedules

        CreateMap<DoctorWorkingScheduleForCreation, DoctorWorkingSchedule>()
            .ForPath(dest => dest.WorkingSchedule, opt => opt.Ignore())
            .ForPath(dest => dest.Doctor, opt => opt.Ignore())
            .ForPath(dest => dest.DoctorId, opt => opt.Ignore())
            .ForPath(dest => dest.WorkingScheduleId, opt => opt.MapFrom(x => x.WorkingScheduleId));

        CreateMap<DoctorWorkingScheduleForUpdate, DoctorWorkingSchedule>()
            .ForPath(dest => dest.WorkingSchedule, opt => opt.Ignore())
            .ForPath(dest => dest.Doctor, opt => opt.Ignore())
            .ForPath(dest => dest.DoctorId, opt => opt.Ignore())
            .ForPath(dest => dest.WorkingScheduleId, opt => opt.MapFrom(x => x.WorkingScheduleId));
        #endregion
    }
}