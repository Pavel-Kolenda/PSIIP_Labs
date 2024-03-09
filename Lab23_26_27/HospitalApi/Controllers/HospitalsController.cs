using Shared.Dtos.Hospitals;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalsController : ControllerBase
{
    private readonly IHospitalsService _hospitalService;

    public HospitalsController(IHospitalsService hospitalService)
    {
        _hospitalService = hospitalService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var hospitals = await _hospitalService.GetAllAsync(false);

        return Ok(hospitals);
    }

    [HttpGet("{id:int}", Name = "GetHospital")]
    public async Task<IActionResult> Get(int id)
    {
        var hospital = await _hospitalService.GetAsync(id, false);

        return Ok(hospital);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HospitalForCreation hospitalForCreation)
    {
        HospitalDto hospital = await _hospitalService.CreateAsync(hospitalForCreation);

        return CreatedAtRoute("GetHospital", new { Id = hospital.Id }, hospital);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] HospitalForUpdate hospitalForUpdate)
    {
        await _hospitalService.UpdateAsync(id, hospitalForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _hospitalService.DeleteAsync(id, true);

        return NoContent();
    }
}
