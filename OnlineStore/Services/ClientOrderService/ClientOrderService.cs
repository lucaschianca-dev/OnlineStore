using AutoMapper;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Models;
using OnlineStore.Repositories;
using OnlineStore.Repositories.ClientOrderRepository;

namespace OnlineStore.Services.ClientOrderService;

public class ClientOrderService
{
    private readonly IMapper _mapper;
    private readonly IClientOrderRepository _clientOrderRepository;
    private readonly IItemRepository _itemRepository;

    public ClientOrderService(IMapper mapper, IClientOrderRepository clientOrderRepository, IItemRepository itemRepository)
    {
        _mapper = mapper;
        _clientOrderRepository = clientOrderRepository;
        _itemRepository = itemRepository;
    }

    //public async Task<AddClientOrderOutput> AddClientOrderAsync(AddClientOrderInput input)
    //{

    //}

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
