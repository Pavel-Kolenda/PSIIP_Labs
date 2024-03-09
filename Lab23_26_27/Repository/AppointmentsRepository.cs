using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repository;
public class AppointmentsRepository : RepositoryBase<Appointment>, IAppointmentsRepository
{
    public AppointmentsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    {
    }

    public async Task CreateAsync(Appointment appointment)
    {
        await AddAsync(appointment);
    }

    public void Remove(Appointment appointment)
    {
        Delete(appointment);
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(bool trackChanges)
    {
        var appointments = await GetAll(trackChanges).ToListAsync();

        return appointments;
    }


    public async Task<Appointment> GetAsync(int id, bool trackChanges)
    {
        var appointment = await Find(appointment => appointment.Id.Equals(id), trackChanges)
                               .FirstOrDefaultAsync();

        return appointment ?? throw new AppointmentNotFoundException($"Appointment with {id} not found");
    }

    public PagedList<Appointment> GetAllWithDoctorAndPatientAsync(
        int hospitalId,
        bool trackChanges,
        RequestParameters requestParameters)
    {
        var appointments = GetAll(trackChanges)
            .Include(d => d.Doctor)
            .Include(p => p.Patient)
            .Include(h => h.Hospital)
            .Where(a => a.HospitalId == hospitalId)
            .Select(ap => CreateAppointment(ap));

        var pagedList = PagedList<Appointment>.Create(appointments, requestParameters.PageNumber, requestParameters.PageSize);

        return pagedList;
    }

    public async Task<Appointment> GetWithDoctorAndPatientAsync(int hospitalId, int id, bool trackChanges)
    {
        var appointment = await Find(ap => ap.Id.Equals(id) && ap.HospitalId.Equals(hospitalId), trackChanges)
                               .Include(d => d.Doctor)
                               .Include(p => p.Patient)
                               .Include(h => h.Hospital)
                               .Select(ap => CreateAppointment(ap))
                               .FirstOrDefaultAsync();

        return appointment ?? throw new AppointmentNotFoundException($"Appointment with {id} not found");
    }

    private static Appointment CreateAppointment(Appointment appointment)
    {
        return new Appointment
        {
            Id = appointment.Id,
            AppointmentTime = appointment.AppointmentTime,
            Doctor = new Doctor
            {
                Name = appointment.Doctor.Name,
                Surname = appointment.Doctor.Surname,
                Patronymic = appointment.Doctor.Patronymic
            },
            Patient = new Patient
            {
                Name = appointment.Patient.Name,
                Surname = appointment.Patient.Surname,
                Patronymic = appointment.Patient.Patronymic
            },
            Hospital = new Hospital
            {
                Id = appointment.Hospital.Id
            }
        };
    }
}
