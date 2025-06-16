using CS.RequestResponse.OrderHistory;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.Web.Controllers;

public class OrderHistoryController:ControllerBase
{
    private readonly IOrderHistoryService _orderHistoryService;

    public OrderHistoryController(IOrderHistoryService orderHistoryService)
    {
        _orderHistoryService = orderHistoryService;
    }

    [HttpGet("GetOrderHistory")]
    public async Task<ActionResult<GetOrderHistoryResponse>> GetOrderHistory(GetOrderHistoryRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var response=await _orderHistoryService.GetOrderHistory(request);
        return Ok(response);
    }
}