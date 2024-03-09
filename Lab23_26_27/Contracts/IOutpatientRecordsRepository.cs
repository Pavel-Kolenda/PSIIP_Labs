using HospitalApi.Entities;

namespace Contracts;
public interface IOutpatientRecordsRepository
{
    Task<OutpatientRecord> GetAsync(int patientId, int outpatientRecordId, bool trackChanges);
    Task CreateAsync(int patientId, OutpatientRecord outpatientRecord);
    void Remove(OutpatientRecord outpatientRecord);
    Task<IEnumerable<OutpatientRecord>> GetAllWithDoctorAsync(int patientId, bool trackChanges);
    Task<OutpatientRecord> GetWithDoctorAsync(int patientId, int outpatientRecordId, bool trackChanges);
}
