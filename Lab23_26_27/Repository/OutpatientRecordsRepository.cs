using Contracts;
using Entities.Exceptions.NotFound;
using HospitalApi.Entities;
using HospitalApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class OutpatientRecordsRepository : RepositoryBase<OutpatientRecord>, IOutpatientRecordsRepository
{
    public OutpatientRecordsRepository(HospitalContext hospitalContext) : base(hospitalContext)
    { 
    }

    public async Task CreateAsync(int patientId, OutpatientRecord outpatientRecord)
    {
        outpatientRecord.PatientId = patientId;
        outpatientRecord.RecordDate = DateTime.Now;

        await AddAsync(outpatientRecord);
    }

    public void Remove(OutpatientRecord outpatientRecord)
    {
        Delete(outpatientRecord);
    }

    public async Task<OutpatientRecord> GetAsync(int patientId, int outpatientRecordId, bool trackChanges)
    {
        var outpatientRecord = await Find(x => x.PatientId.Equals(patientId), trackChanges)
                                    .Where(x => x.Id.Equals(outpatientRecordId))
                                    .FirstOrDefaultAsync();

        return outpatientRecord ?? throw new OutpatientRecordNotFound($"Outpatient Record with Id:{outpatientRecordId} not found");
    }

    public async Task<OutpatientRecord> GetWithDoctorAsync(int patientId, int outpatientRecordId, bool trackChanges)
    {
        var outpatientRecord = await Find(x => x.PatientId.Equals(patientId), trackChanges)
                                    .Include(doc => doc.Doctor)
                                    .Include(p => p.Patient)
                                    .Where(x => x.Id.Equals(outpatientRecordId))
                                    .Select(or => CreateOutpatientRecord(or))
                                    .FirstOrDefaultAsync();
        return outpatientRecord ?? throw new OutpatientRecordNotFound($"Outpatient Record with Id:{outpatientRecordId} not found");
    }

    public async Task<IEnumerable<OutpatientRecord>> GetAllWithDoctorAsync(int patientId, bool trackChanges)
    {
        var outpatientRecords = await Find(x => x.PatientId.Equals(patientId), trackChanges)
                                     .Include(d => d.Doctor)
                                     .Include(p => p.Patient)
                                     .Select(or => CreateOutpatientRecord(or))
                                     .ToListAsync();

        return outpatientRecords;
    }

    private static OutpatientRecord CreateOutpatientRecord(OutpatientRecord outpatientRecord)
    {
        return new OutpatientRecord
        {
            Id = outpatientRecord.Id,
            Description = outpatientRecord.Description,
            RecordDate = outpatientRecord.RecordDate,

            Doctor = new Doctor
            {
                Name = outpatientRecord.Doctor.Name,
                Surname = outpatientRecord.Doctor.Surname
            },
            Patient = new Patient
            {
                Name = outpatientRecord.Patient.Name,
                Surname = outpatientRecord.Patient.Surname
            }
        };
    }
}
