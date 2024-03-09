using Shared.Dtos.Appointments;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/hospitals/{hospitalId:int}/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _appointmentService;

    public AppointmentsController(IAppointmentsService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public IActionResult Get(int hospitalId, [FromQuery] RequestParameters requestParameters)
    {
        var appointments = _appointmentService.GetWithDoctorAndPatientAsync(hospitalId, false, requestParameters);

        return Ok(appointments);
    }

    [HttpGet("{id:int}", Name = "GetById")]
    public async Task<IActionResult> Get(int hospitalId, int id)
    {
        var appointment = await _appointmentService.GetWithDoctorAndPatientAsync(hospitalId, id, false);

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AppointmentForCreation appointmentForCreation)
    {
        AppointmentDto appointment = await _appointmentService.CreateAsync(appointmentForCreation);

        return CreatedAtRoute("GetById", new { hospitalId = appointment.HospitalNumber, id = appointment.Id }, appointment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AppointmentForUpdate appointmentForUpdate)
    {
        await _appointmentService.UpdateAsync(id, appointmentForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appointmentService.DeleteAsync(id, true);

        return NoContent();
    }
}