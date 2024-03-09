using Shared.Dtos.Doctors;
using System.Web;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/hospitals/{hospitalId:int}/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsService _doctorService;

    public DoctorsController(IDoctorsService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public IActionResult Get(int hospitalId, [FromQuery] RequestParameters requestParameters)
    {
        var doctors = _doctorService.GetWithHospitalAndSpecializationAsync(hospitalId, false, requestParameters);

        return Ok(doctors);
    }

    [HttpGet("{id:int}", Name = "GetDoctor")]
    public async Task<IActionResult> Get(int hospitalId, int id)
    {
        var doctor = await _doctorService.GetWithHospitalAndSpecializationAsync(hospitalId, id, false);

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DoctorForCreation doctorForCreation)
    {
        var doctor = await _doctorService.CreateAsync(doctorForCreation);

        return CreatedAtRoute("GetDoctor", new { hospitalId = doctor.Hospital, id = doctor.Id }, doctor);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] DoctorForUpdate doctorForUpdate)
    {
        await _doctorService.UpdateAsync(id, doctorForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _doctorService.DeleteAsync(id, false);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetByName(int hospitalId, [FromQuery] string name, [FromQuery] string surname)
    {
        if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(surname))
        {
            var decodedName = HttpUtility.UrlDecode(name);
            var decodedSurname = HttpUtility.UrlDecode(surname);
            var doctor = await _doctorService.GetDoctorByNameAndSurname(hospitalId, decodedName, decodedSurname);

            return Ok(doctor);
        }

        return NoContent();       
    }
}
