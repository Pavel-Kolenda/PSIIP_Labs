using Shared.Dtos.DoctorWorkingSchedules;
using System.Net;

namespace HospitalApi.Controllers;
[ApiController]
[Route("api/hospitals/{hospitalId:int}/doctors/")]
public class DoctorWorkingSchedulesController : ControllerBase
{
    private readonly IDoctorWorkingSchedulesService _doctorWorkingScheduleService;

    public DoctorWorkingSchedulesController(IDoctorWorkingSchedulesService doctorWorkingScheduleService)
    {
        _doctorWorkingScheduleService = doctorWorkingScheduleService;
    }

    [HttpGet("{doctorId:int}/schedules", Name = "GetDoctorWorkingSchedule")]
    public async Task<IActionResult> Get(int hospitalId, int doctorId)
    {
        var dws = await _doctorWorkingScheduleService.GetAsync(hospitalId, doctorId, false);

        return Ok(dws);
    }

    [HttpGet("schedules")]
    public async Task<IActionResult> GetAll(int hospitalId)
    {
        var dws = await _doctorWorkingScheduleService.GetAllAsync(hospitalId, false);

        return Ok(dws);
    }

    [HttpPost("{doctorId:int}/schedules")]
    public async Task<IActionResult> Create(int hospitalId, int doctorId, [FromBody] DoctorWorkingScheduleForCreation dwsForCreation)
    {
        await _doctorWorkingScheduleService.CreateAsync(hospitalId, doctorId, dwsForCreation);

        return CreatedAtRoute("GetDoctorWorkingSchedule", 
            new { hospitalId = hospitalId, doctorId = doctorId, id = dwsForCreation.WorkingScheduleId }, dwsForCreation);
    }

    [HttpPut("{doctorId:int}/schedules/{dwsId:int}")]
    public async Task<IActionResult> Update(int doctorId, int dwsId, [FromBody] DoctorWorkingScheduleForUpdate dwsForUpdate)
    {
        await _doctorWorkingScheduleService.UpdateAsync(doctorId, dwsId, dwsForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{doctorId:int}/schedules/{dwsId:int}")]
    public async Task<IActionResult> Delete(int doctorId, int dwsId)
    {
        await _doctorWorkingScheduleService.DeleteAsync(doctorId, dwsId, true);

        return NoContent();
    }

    [HttpGet("schedules/word")]
    public async Task<IActionResult> GetPdf(int hospitalId)
    {
        var fileBytes = await _doctorWorkingScheduleService.GenerateWordAsync(hospitalId);

        return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "DoctorSchedules.docx");
    }
}
