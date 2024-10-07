using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Models;
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

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] AddClientOrderInput input)
    {
        if (input == null || input.Items == null || input.Items.Count == 0)
        {
            return BadRequest("A lista de itens não pode estar vazia.");
        }

        try
        {
            bool orderCreated = await _clientOrderService.CreateClientOrderAsync(input);

            if (orderCreated)
            {
                return Ok(new { message = "Pedido criado com sucesso!" });
            }
            else
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Erro ao criar o pedido: {ex.Message}");
        }
    }

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
