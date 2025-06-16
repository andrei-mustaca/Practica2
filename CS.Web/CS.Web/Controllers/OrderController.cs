using CS.RequestResponse.Order;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController:ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost("CreateOrder")]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response =await _service.Create(request);
        return Ok(response);
    }

    [HttpPost("CreateAcceptanceOrder")]
    public async Task<ActionResult<CreateOrderAcceptanceResponse>> CreateOrderAcceptance(
        [FromBody] CreateOrderAcceptanceRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var response=await _service.CreateAcceptance(request);
        return Ok(response);
    }

    [HttpPost("CreatePayment")]
    public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response =await _service.CreatePayment(request);
        return Ok(response);
    }

    [HttpGet("GetClientOrder")]
    public async Task<ActionResult<GetClientOrdersResponse>> GetClientOrders([FromQuery]GetClientOrdersRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response = await _service.GetClientOrders(request);
        return Ok(response);
    }

    [HttpGet("GetCourierOrders")]
    public async Task<ActionResult<GetCourierOrdersResponse>> GetCourierOrders(
        [FromQuery] GetCourierOrdersRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response = await _service.GetCourierOrders(request);
        return Ok(response);
    }

    [HttpGet("GetFreeOrders")]
    public async Task<ActionResult<GetOrdersResponse>> GetOrders([FromQuery] GetOrdersRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var response = await _service.GetOrders(request);
        return Ok(response);
    }
}