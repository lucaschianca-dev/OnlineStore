using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.DTOs.Item.AtualizarItem;
using OnlineStore.DTOs.Item.CriarItem;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        try
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao recuperar itens: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemsById(string id)
    {
        var item = await _itemService.GetItemByIdAsync(id);

        if (item == null)
        {
            return NotFound(new { message = "Item não encontrado" });
        }
        return Ok(item);
    }

    [HttpPost("user/{userId}")]
    public async Task<IActionResult> AddItemToUser(string userId, [FromBody] CriarItemInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _itemService.AddItemToUserAsync(userId, input);

            if (result.Sucesso)
            {
                return CreatedAtAction(nameof(GetItemsById), new { id = result.Id }, result);
            }
            else
            {
                return BadRequest(result.MensagemErro);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar item: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] CriarItemInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _itemService.AddItemAsync(input);

            if (result.Sucesso)
            {
                return CreatedAtAction(nameof(GetItems), new { id = result.Id }, result);
            }
            else
            {
                return BadRequest(result.MensagemErro);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao adicionar item: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateItemPartially(string id, [FromBody] AtualizarItemInput input)
    {
        var result = await _itemService.UpdateItemAsync(id, input);

        if (result)
        {
            return Ok(new { message = "Item atualizado com sucesso" });
        }

        return NotFound(new { message = "Item não encontrado" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(string id)
    {
        var result = await _itemService.DeleteItemAsync(id);

        if (result)
        {
            return Ok(new { message = "Item deletado com sucesso" });
        }

        return NotFound(new { message = "Item não encontrado" });
    }
}

