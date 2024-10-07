namespace OnlineStore.DTOs.Item.CriarItem;

public interface ICriarItem
{
    Task<CriarItemOutput> Executar(CriarItemInput input);
}