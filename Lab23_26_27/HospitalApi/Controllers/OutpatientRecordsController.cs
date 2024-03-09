using Shared.Dtos.OutpatientRecords;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/patients/{patientId:int}/[controller]")]
public class OutpatientRecordsController : ControllerBase
{
    private readonly IOutpatientRecordsService _outpatientService;

    public OutpatientRecordsController(IOutpatientRecordsService outpatientService)
    {
        _outpatientService = outpatientService;
    }

    [HttpGet(Name = "GetOutpatientRecords")]
    public async Task<IActionResult> Get(int patientId)
    {
        var outpatientRecords = await _outpatientService.GetWithDoctorAsync(patientId, false);

        return Ok(outpatientRecords);
    }


    [HttpGet("{outpatientRecordId:int}")]
    public async Task<IActionResult> Get(int patientId, int outpatientRecordId)
    {
        var outpatientRecord = await _outpatientService.GetWithDoctorAsync(patientId, outpatientRecordId, false);

        return Ok(outpatientRecord);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int patientId, [FromBody] OutpatientRecordForCreation outpatientRecordForCreation)
    {
        OutpatientRecordDto outpatientRecord = await _outpatientService.CreateAsync(patientId, outpatientRecordForCreation);

        return CreatedAtRoute("Get", new { Id = outpatientRecord.Id }, outpatientRecord);
    }

    [HttpPut("{outpatientRecordId:int}")]
    public async Task<IActionResult> Update(int patientId,
        int outpatientRecordId,
        [FromBody] OutpatientRecordForUpdate outpatientRecordForUpdate)
    {
        await _outpatientService.UpdateAsync(patientId, outpatientRecordId, outpatientRecordForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{outpatientRecordId:int}")]
    public async Task<IActionResult> Delete(int patientId, int outpatientRecordId)
    {
        await _outpatientService.DeleteAsync(patientId, outpatientRecordId, true);

        return NoContent();
    }
}
