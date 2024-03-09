using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared;
using System.Linq.Dynamic.Core;

namespace Repository;
public class DoctorsRepository : RepositoryBase<Doctor>, IDoctorsRepository
{
    
    public DoctorsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(Doctor doctor, int specializationId)
    {
        doctor.SpecializationId = specializationId;
        await AddAsync(doctor);
    }

    public void Remove(Doctor doctor)
    {
        Delete(doctor);
    }

    public IQueryable<Doctor> GetAllAsync(bool trackChanges)
    {
        var doctors = GetAll(trackChanges);

        return doctors;
    }

    public async Task<Doctor> GetAsync(int id, bool trackChanges)
    {
        var doctor = await Find(doctor => doctor.Id.Equals(id), trackChanges)
                          .FirstOrDefaultAsync();

        return doctor ?? throw new DoctorNotFoundException($"Doctor with Id:{id} not found");
    }

    public IEnumerable<Doctor> GetAllWithHospitalAndSpecialization(int hospitalId, bool trackChanges, RequestParameters requestParameters)
    {
        var doctors = GetAll(trackChanges)
            .Include(d => d.Hospital)
            .Include(d => d.Specialization)
            .Where(d => d.HospitalId == hospitalId)
            .Sort(requestParameters.SortBy)
            .Select(doc => CreateDoctor(doc));

        var pagedList = PagedList<Doctor>.Create(doctors, requestParameters.PageNumber, requestParameters.PageSize);

        return pagedList;
    }

    public async Task<Doctor> GetWithHospitalAndSpecializationAsync(int hospitalId, int id, bool trackChanges)
    {
        var doctor = await Find(doctor => doctor.Id.Equals(id) && doctor.HospitalId == hospitalId, trackChanges)
                          .Include(d => d.Hospital)
                          .Include(d => d.Specialization)
                          .Select(doc => CreateDoctor(doc))
                          .FirstOrDefaultAsync();

        return doctor ?? throw new DoctorNotFoundException($"Doctor with Id:{id} not found");
    }

    public async Task<int> GetIdByFullNameAsync(string firstName, string surname)
    {
        int doctorId = await _hospitalContext
                            .Doctors
                            .Where(doc => doc.Name!.Equals(firstName) && doc.Surname!.Equals(surname))
                            .Select(doc => doc.Id)
                            .FirstOrDefaultAsync();

        if (doctorId == 0)
            throw new DoctorNotFoundException($"Doctor with {firstName} {surname} not found");

        return doctorId;
    }

    private static Doctor CreateDoctor(Doctor doctor)
    {
        return new Doctor
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Surname = doctor.Surname,
            Patronymic = doctor.Patronymic,

            Hospital = new Hospital
            {
                Id = doctor.HospitalId,
                Address = doctor.Hospital.Address,
            },
            Specialization = new Specialization
            {
                SpecializationName = doctor.Specialization.SpecializationName
            }
        };
    }
}
