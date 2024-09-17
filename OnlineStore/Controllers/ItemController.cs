using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.Item.CriarItem;
using OnlineStore.Services;
using System.Threading.Tasks;

namespace OnlineStore.Controllers;
    
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemsController(ItemService itemService)
    {
        _itemService = itemService;
    }

    // POST: api/Items
    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] CriarItemInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        CriarItemOutput output = await _itemService.AddItemAsync(input);
        if (output.Sucesso)
        {
            return CreatedAtAction(nameof(GetItemById), new { id = output.Id }, output);
        }
        else
        {
            return BadRequest(output);
        }
    }

    // GET: api/Items/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(string id)
    {
        var output = await _itemService.GetItemByIdAsync(id);
        if (output != null)
        {
            return Ok(output);
        }

        return NotFound($"Item with ID {id} not found.");
    }
}