using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Services.ClientOrderService;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientOrderController : ControllerBase
{
    private readonly ClientOrderService _clientOrderService;

    public ClientOrderController(ClientOrderService clientOrderService)
    {
        _clientOrderService = clientOrderService;
    }

    //[HttpPost]
    //public async Task<IActionResult> AddClientOrder([FromBody] AddClientOrderInput input)
    //{

    //}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientOrderById(string id)
    {
        var order = await _clientOrderService.GetClientOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound(new { message = "Pedido não encontrado" });
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClientOrders()
    {
        var orders = await _clientOrderService.GetAllClientOrdersAsync();
        return Ok(orders);
    }
}
