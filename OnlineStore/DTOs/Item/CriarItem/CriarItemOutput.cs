namespace OnlineStore.DTOs.Item.CriarItem;

public class CriarItemOutput
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public bool Sucesso { get; set; }
    public string MensagemErro { get; set; }
}