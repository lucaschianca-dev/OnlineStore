using AutoMapper;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Models;
using OnlineStore.Repositories.ClientOrderRepository;
using OnlineStore.Repositories.ItemRepository;
using OnlineStore.Repositories.UserRepository;

namespace OnlineStore.Services.ClientOrderService;

public class ClientOrderService
{
    private readonly IMapper _mapper;
    private readonly IClientOrderRepository _clientOrderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;

    public ClientOrderService(IMapper mapper, IClientOrderRepository clientOrderRepository, IItemRepository itemRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _clientOrderRepository = clientOrderRepository;
        _itemRepository = itemRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> CreateClientOrderAsync(AddClientOrderInput input)
    {
        var itemsOrder = new List<ItemOrder>();

        foreach (var itemInput in input.Items)
        {
            // Buscar o item pelo ItemId para capturar o preço atual
            var item = await _itemRepository.GetItemByIdAsync(itemInput.ItemId);
            if (item == null || !item.IsAvailable || item.StockQuantity < itemInput.Quantity)
            {
                return false; // Verificar se o item é válido e está disponível
            }

            // Criar o ItemOrder com o preço do back-end
            var itemOrder = _mapper.Map<ItemOrder>(itemInput);
            itemOrder.UnitPrice = item.Price; // O preço do item é obtido no back-end
            itemsOrder.Add(itemOrder);
        }

        double totalAmount = itemsOrder.Sum(i => i.TotalPrice);

        var order = _mapper.Map<ClientOrder>(input);
        order.Items = itemsOrder; // Substituir os itens mapeados
        order.TotalAmount = totalAmount; // Atribuir o valor total calculado
        order.Status = "Pendente";
        order.OrderDate = DateTime.UtcNow;

        string orderId = await _clientOrderRepository.CreateClientOrderAsync(order);
        order.Id = orderId;

        var user = await _userRepository.GetUserByIdAsync(input.UserId);
        user.ClientOrderIds.Add(orderId);
        await _userRepository.UpdateUserAsync(user.Id, user);

        return true;
    }

    public async Task<AddClientOrderOutput> GetClientOrderByIdAsync(string id)
    {
        var clientOrder = await _clientOrderRepository.GetClientOrderByUserIdAsync(id);
        if (clientOrder == null)
        {
            return null;
        }

        return _mapper.Map<AddClientOrderOutput>(clientOrder);
    }

    public async Task<List<AddClientOrderOutput>> GetAllClientOrdersAsync()
    {
        var clientOrders = await _clientOrderRepository.GetAllClientOrdersAsync();
        return _mapper.Map<List<AddClientOrderOutput>>(clientOrders);
    }
}
