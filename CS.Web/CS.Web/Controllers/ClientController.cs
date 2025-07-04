using CS.RequestResponse.Client;
using CS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.Web.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class ClientController : ControllerBase 
{
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("CreateClient")]
        public async Task<ActionResult<CreateClientResponse>> Create([FromBody] CreateClientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _clientService.Create(request);
            return Ok(response); 
        }
        
        [HttpPost("UpdateName")]
        public async Task<ActionResult<CreateClientResponse>> UpdateName([FromBody] UpdateNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _clientService.UpdateName(request);
            return Ok(response); 
        }
        
        [HttpPost("UpdateEmail")]
        public async Task<ActionResult<CreateClientResponse>> UpdateEmail([FromBody] UpdateEmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _clientService.UpdateEmail(request);
            return Ok(response); 
        }
}