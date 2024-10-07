namespace OnlineStore.DTOs.ItemDto.CriarItem;

public class CriarItemOutput
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public bool Sucesso { get; set; }
    public string MensagemErro { get; set; }
}