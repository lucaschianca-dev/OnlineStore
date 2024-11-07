namespace OnlineStore.DTOs.ItemDto.AtualizarItem;

public class AtualizarItemInput
{
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
    public int? StockQuantity { get; set; }
    public bool? IsAvailable { get; set; }
}
