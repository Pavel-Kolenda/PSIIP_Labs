using Shared.Dtos.HospitalAddresses;

namespace HospitalApi.Controllers;

[ApiController]
[Route("api/hospitals/{hospitalId:int}/[controller]")]
public class HospitalAddressesController : ControllerBase
{
    private readonly IHospitalAddressesService _hospitalAddressService;

    public HospitalAddressesController(IHospitalAddressesService hospitalAddressService)
    {
        _hospitalAddressService = hospitalAddressService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int hospitalId)
    {
        var hospitalAddresses = await _hospitalAddressService.GetAllAsync(hospitalId, false);

        return Ok(hospitalAddresses);
    }

    [HttpGet("{id:int}", Name = "GetHospitalAddress")]
    public async Task<IActionResult> Get(int hospitalId, int id)
    {
        var hospitalAddress = await _hospitalAddressService.GetAsync(hospitalId, id, false);

        return Ok(hospitalAddress);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int hospitalId, [FromBody] HospitalAddressForCreation hospitalAddressForCreation)
    {
        HospitalAddressDto hospitalAddress = await _hospitalAddressService.CreateAsync(hospitalId, hospitalAddressForCreation);

        return CreatedAtRoute("GetHospitalAddress", new { hospitalId = hospitalId, id = hospitalAddress.Id }, hospitalAddress);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int hospitalId, int id, HospitalAddressForUpdate hospitalAddressForUpdate)
    {
        await _hospitalAddressService.UpdateAsync(hospitalId, id, hospitalAddressForUpdate, true);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int hospitalId, int id)
    {
        await _hospitalAddressService.DeleteAsync(hospitalId, id, true);

        return NoContent();
    }
}
