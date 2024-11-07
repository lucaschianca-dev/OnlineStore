using OnlineStore.DTOs.ItemOrderDto;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.ClientOrderDto.AddClientOrder;

public class AddClientOrderInput
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public List<ItemOrderInput> Items { get; set; }
}
