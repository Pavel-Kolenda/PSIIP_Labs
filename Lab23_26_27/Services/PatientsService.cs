using AutoMapper;
using Contracts;
using HospitalApi.Entities;
using Services.Contracts;
using Shared.Dtos.Patients;

namespace Services;
public class PatientsService : IPatientsService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public PatientsService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<PatientDto> CreateAsync(PatientForCreation patientForCreate)
    {
        (int hospitalAddressId, int patientHospitalId) = await _repositoryManager
            .HospitalAddressesRepository
            .GetAddressIdAndHospitalIds(patientForCreate.Address, patientForCreate.City, patientForCreate.Country);

        var patient = _mapper.Map<Patient>(patientForCreate);

        await _repositoryManager.PatientsRepository.CreateAsync(patient, hospitalAddressId, patientHospitalId);

        await _repositoryManager.SaveChangesAsync();

        PatientDto patientToReturn = _mapper.Map<PatientDto>(patient);

        return patientToReturn;
    }

    public async Task DeleteAsync(int hospitalId, int id, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetAsync(hospitalId, id, trackChanges);

        _repositoryManager.PatientsRepository.Remove(patient);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task<PatientDto> GetWithAddressAsync(int hospitalId, int id, bool trackChanges)
    {
        var patient = await _repositoryManager
            .PatientsRepository
            .GetWithAddressAsync(hospitalId, id, trackChanges);

        var patientDto = _mapper.Map<PatientDto>(patient);

        return patientDto;
    }

    public IEnumerable<PatientDto> GetAllWithAddressAsync(int hospitalId, bool trackChanges)
    {
        var patients = _repositoryManager
            .PatientsRepository
            .GetAllWithAddressAsync(hospitalId, trackChanges)
            .ToList();

        var patientsDto = _mapper.Map<IEnumerable<PatientDto>>(patients);

        return patientsDto;
    }

    public async Task UpdateAsync(int hospitalId, int id, PatientForUpdate patientForUpdate, bool trackChanges)
    {
        var patient = await _repositoryManager.PatientsRepository.GetAsync(hospitalId, id, trackChanges);

        (int addressId, int newHospitalId)= await _repositoryManager
            .HospitalAddressesRepository
            .GetAddressIdAndHospitalIds(patientForUpdate.Address, patientForUpdate.City, patientForUpdate.Country);

        patient.AddressId = addressId;
        patient.HospitalId = newHospitalId;

        _mapper.Map(patientForUpdate, patient);

        await _repositoryManager.SaveChangesAsync();
    }

    public bool DoesLiveOnAddress(int hospitalId, string address, string firstName, string surname)
    {
        var patients = _repositoryManager
            .PatientsRepository
            .GetAllWithAddressAsync(hospitalId, true)
            .Any(x => x.Address.Address!.Contains(address)
              && x.Name.Equals(firstName) 
              && x.Surname.Equals(surname));

        return patients;
    }
}
