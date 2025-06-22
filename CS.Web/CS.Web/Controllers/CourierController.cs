using CS.RequestResponse.Courier;
using CS.Services;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.Web.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class CourierController:ControllerBase
{
    private readonly ICourierService _courierService;

    public CourierController(ICourierService service)
    {
        _courierService = service;
    }

    [HttpPost("CreateCourier")]
    public async Task<ActionResult<CreateCourierResponse>> CreateCourier([FromBody] CreateCourierRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response =await _courierService.CreateCourier(request);
        return Ok(response);
    }
    
    [HttpPost("UpdateName")]
    public async Task<ActionResult<CreateCourierResponse>> UpdateName([FromBody] UpdateNameCourier request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _courierService.UpdateName(request);
        return Ok(response); 
    }
}