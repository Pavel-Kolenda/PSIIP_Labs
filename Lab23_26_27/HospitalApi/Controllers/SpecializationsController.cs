using Shared.Dtos.Specializations;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationsService _specializationService;

    public SpecializationsController(ISpecializationsService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var specializations = await _specializationService.GetAllAsync(false);

        return Ok(specializations);
    }

    [HttpGet("{id:int}", Name = "GetSpecialization")]
    public async Task<IActionResult> Get(int id)
    {
        var specialization = await _specializationService.GetAsync(id, false);

        return Ok(specialization);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SpecializationForCreation specializationForCreation)
    {
        SpecializationDto specialization = await _specializationService.CreateAsync(specializationForCreation);

        return CreatedAtRoute("GetSpecialization", new { id = specialization.Id }, specialization);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SpecializationForUpdate specializationForUpdate)
    {
        await _specializationService.UpdateAsync(id, specializationForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _specializationService.DeleteAsync(id, true);

        return NoContent(); 
    }
}
