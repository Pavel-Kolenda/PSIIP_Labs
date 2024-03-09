using Shared.Dtos.WorkingSchedules;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkingSchedulesController : ControllerBase
{
    private readonly IWorkingSchedulesService _workingScheduleService;

    public WorkingSchedulesController(IWorkingSchedulesService workingScheduleService)
    {
        _workingScheduleService = workingScheduleService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var workingSchedules = await _workingScheduleService.GetAllAsync(false);

        return Ok(workingSchedules);
    }

    [HttpGet("{id:int}", Name = "GetWorkingSchedule")]
    public async Task<IActionResult> Get(int id)
    {
        var workingSchedule = await _workingScheduleService.GetAsync(id, false);

        return Ok(workingSchedule);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkingScheduleForCreation workingScheduleForCreation)
    {
        WorkingScheduleDto workingSchedule = await _workingScheduleService.CreateAsync(workingScheduleForCreation);

        return CreatedAtRoute("GetWorkingSchedule", new { id = workingSchedule.Id }, workingSchedule);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] WorkingScheduleForUpdate workingScheduleForUpdate)
    {
        await _workingScheduleService.UpdateAsync(id, workingScheduleForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _workingScheduleService.DeleteAsync(id, true);

        return NoContent();
    }
}
