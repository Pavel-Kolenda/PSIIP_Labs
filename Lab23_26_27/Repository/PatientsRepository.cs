using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class PatientsRepository : RepositoryBase<Patient>, IPatientsRepository
{
    public PatientsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(Patient patient, int addressId, int hospitalId)
    {
        patient.AddressId = addressId;
        patient.HospitalId = hospitalId;
        await AddAsync(patient);
    }

    public void Remove(Patient patient)
    {
        Delete(patient);
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(int hospitalId, bool trackChanges)
    {
        var patients = await GetAll(trackChanges)
            .Where(p => p.HospitalId == hospitalId)
            .ToListAsync();

        return patients;
    }

    public async Task<Patient> GetAsync(int hospitalId, int id, bool trackChanges)
    {
        var patient = await Find(p => p.Id == id && p.HospitalId == hospitalId, trackChanges)
            .FirstOrDefaultAsync();

        return patient ?? throw new PatientNotFoundException($"Patient with id:{id} not found");
    }

    public async Task<Patient> GetWithAddressAsync(int hospitalId, int id, bool trackChanges)
    {
        var patient = Find(patient => patient.Id == id && patient.HospitalId == hospitalId, trackChanges)
            .Include(p => p.Address)
            .Select(p => CreatePatient(p));

        return await patient.FirstOrDefaultAsync() ?? throw new PatientNotFoundException($"Patient with Id:{id} not found");
    }

    public IQueryable<Patient> GetAllWithAddressAsync(int hospitalId, bool trackChanges)
    {
        var patients = GetAll(trackChanges)
            .Where(p => p.HospitalId == hospitalId)
            .Include(p => p.Address)
            .Select(p => CreatePatient(p));

        return patients;
    }

    public async Task<int> GetIdByFullNameAsync(string name, string surname)
    {
        var patientId = await _hospitalContext.Patients
                            .Where(p => p.Name.Equals(name) && p.Surname.Equals(surname))
                            .Select(p => p.Id)
                            .FirstOrDefaultAsync();

        if (patientId == 0)
            throw new PatientNotFoundException($"Patient with Name:{name} and Surname:{surname} not found");
        return patientId;
    }


    private static Patient CreatePatient(Patient p)
    {
        return new Patient
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            Patronymic = p.Patronymic,
            PhoneNumber = p.PhoneNumber,
            HospitalId = p.HospitalId,
            
            Address = new HospitalAddress
            {
                Id = p.Address.Id,
                City = p.Address.City,
                Country = p.Address.Country,
                Address = p.Address.Address
            }
        };
    }

}
