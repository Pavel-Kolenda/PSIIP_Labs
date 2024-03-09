using AutoMapper;
using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using Services.Contracts;
using Shared;
using Shared.Dtos.Doctors;

namespace Services;
public class DoctorsService : IDoctorsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public DoctorsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<DoctorDto> CreateAsync(DoctorForCreation doctorForCreation)
    {
        var specializationId = await _repositoryManager
            .SpecializationsRepository
            .GetIdByNameAsync(doctorForCreation.Specialization, false);


        var doctor = _mapper.Map<Doctor>(doctorForCreation);

        await _repositoryManager.DoctorsRepository.CreateAsync(doctor, specializationId);

        await _repositoryManager.SaveChangesAsync();

        var doctorToReturn = _mapper.Map<DoctorDto>(doctor);

        return doctorToReturn;
    }

    public async Task DeleteAsync(int id, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository.GetAsync(id, trackChanges);

        _repositoryManager.DoctorsRepository.Remove(doctor);

        await _repositoryManager.SaveChangesAsync();

    }

    public async Task<DoctorDto> GetWithHospitalAndSpecializationAsync(int hospitalId, int id, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository
            .GetWithHospitalAndSpecializationAsync(hospitalId, id, trackChanges);

        var doctorDto = _mapper.Map<DoctorDto>(doctor);

        return doctorDto;
    }

    public IEnumerable<DoctorDto> GetWithHospitalAndSpecializationAsync(int hospitalId,
        bool trackChanges, 
        RequestParameters requestParameters)
    {
        var doctors = _repositoryManager
            .DoctorsRepository
            .GetAllWithHospitalAndSpecialization(hospitalId, trackChanges, requestParameters);

        var doctorsDto = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

        return doctorsDto;
    }

    public async Task UpdateAsync(int id, DoctorForUpdate doctorForUpdate, bool trackChanges)
    {
        var doctor = await _repositoryManager.DoctorsRepository.GetAsync(id, trackChanges);

        var specializationId = await _repositoryManager
            .SpecializationsRepository
            .GetIdByNameAsync(doctorForUpdate.Specialization, trackChanges);

        doctor.SpecializationId = specializationId;

        _mapper.Map(doctorForUpdate, doctor);

        await _repositoryManager.SaveChangesAsync();

        using (StreamWriter _logger = new("log.txt", true))
        {
            _logger.WriteLine($"{DateTime.UtcNow}. Изменение доктора c Id {doctor.Id}");
        }
    }

    public async Task<DoctorDto> GetDoctorByNameAndSurname(int hospitalId, string name, string surname)
    {
        var doctorId = await _repositoryManager
            .DoctorsRepository
            .GetIdByFullNameAsync(name, surname);

        var doctor = await _repositoryManager.DoctorsRepository.GetWithHospitalAndSpecializationAsync(hospitalId, doctorId, false);

        var doctorDto = _mapper.Map<DoctorDto>(doctor);

        return doctorDto;
    }

    public int GetIdByHospitalAndSpecialization(int hospitalId, int specializationId)
    {
        int doctorId = _repositoryManager.DoctorsRepository.GetAllAsync(false)
            .Where(d => d.HospitalId.Equals(hospitalId) && d.SpecializationId.Equals(specializationId))
            .Select(d => d.Id)
            .FirstOrDefault();

        if (doctorId == 0)
        {
            throw new DoctorNotFoundException("Doctor not found");
        }

        return doctorId;
    }
}
