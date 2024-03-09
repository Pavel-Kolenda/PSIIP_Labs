using Shared.Dtos.Patients;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/hospitals/{hospitalId:int}/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientsService _patientService;

    public PatientsController(IPatientsService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public IActionResult Get(int hospitalId)
    {
        var patients = _patientService.GetAllWithAddressAsync(hospitalId, false);

        return Ok(patients);
    }

    [HttpGet("{id:int}", Name = "GetPatient")]
    public async Task<IActionResult> Get(int hospitalId, int id)
    {
        var patient = await _patientService.GetWithAddressAsync(hospitalId, id, false);

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PatientForCreation patientForCreate)
    {
        PatientDto patient = await _patientService.CreateAsync(patientForCreate);

        return CreatedAtRoute("GetPatient", new { hospitalId = patient.HospitalId, id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int hospitalId, int id, [FromBody] PatientForUpdate patientForUpdate)
    {
        await _patientService.UpdateAsync(hospitalId, id, patientForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int hospitalId, int id)
    {
        await _patientService.DeleteAsync(hospitalId, id, false);

        return NoContent();
    }
}
