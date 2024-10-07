using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.ItemOrderDto;

public class ItemOrderInput
{
    [Required]
    public string ItemId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
