using OnlineStore.Models;

namespace OnlineStore.Repositories.ClientOrderRepository;

public interface IClientOrderRepository
{
    Task<string> CreateClientOrderAsync(ClientOrder order); // Cria um novo pedido
    Task<ClientOrder> GetClientOrderByUserIdAsync(string userId);  // Recuperar uma ordem pelo ID do usuário
    Task UpdateClientOrderAsync(ClientOrder order);  // Atualizar uma ordem existente
    Task DeleteClientOrderAsync(string id);  // Excluir uma ordem pelo ID
    Task<List<ClientOrder>> GetAllClientOrdersAsync();
}
