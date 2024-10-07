using OnlineStore.Models;
using OnlineStore.DTOs.ItemDto.CriarItem;
using AutoMapper;
using OnlineStore.DTOs.ItemDto.AtualizarItem;
using OnlineStore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.Repositories.ClientOrderRepository;

namespace OnlineStore.Services
{
    public class ItemService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IClientOrderRepository _clientOrderRepository;

        public ItemService(IMapper mapper, IUserRepository userRepository, IItemRepository itemRepository, IClientOrderRepository clientOrderRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _clientOrderRepository = clientOrderRepository;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetItemsAsync();
        }

        public async Task<Item> GetItemByIdAsync(string id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task<CriarItemOutput> AddItemAsync(CriarItemInput input)
        {
            var item = _mapper.Map<Item>(input);
            item.CreationAt = DateTime.UtcNow;  // Garantir que a data seja UTC
            item.UpdatedAt = DateTime.UtcNow;  // Garantir que a data seja UTC

            try
            {
                string id = await _itemRepository.AddItemAsync(item);
                item.Id = id;

                var output = _mapper.Map<CriarItemOutput>(item);
                output.Sucesso = true;
                return output;
            }
            catch (Exception ex)
            {
                return new CriarItemOutput
                {
                    Sucesso = false,
                    MensagemErro = ex.Message
                };
            }
        }

        public async Task<bool> UpdateItemAsync(string id, AtualizarItemInput input)
        {
            var existingItem = await _itemRepository.GetItemByIdAsync(id);
            if (existingItem == null)
            {
                return false; // Ou lançar uma exceção de item não encontrado
            }

            // Apenas atualize os campos se eles foram fornecidos no input
            if (!string.IsNullOrEmpty(input.Name))
            {
                existingItem.Name = input.Name;
            }

            if (input.Price.HasValue)
            {
                existingItem.Price = input.Price.Value;
            }

            if (!string.IsNullOrEmpty(input.Description))
            {
                existingItem.Description = input.Description;
            }

            if (input.StockQuantity.HasValue)
            {
                existingItem.StockQuantity = input.StockQuantity.Value;
            }

            if (input.IsAvailable.HasValue)
            {
                existingItem.IsAvailable = input.IsAvailable.Value;
            }

            // Atualiza o campo UpdatedAt para a data atual
            existingItem.UpdatedAt = DateTime.UtcNow;

            // Chama o repositório para aplicar as alterações
            return await _itemRepository.UpdateItemAsync(id, existingItem);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }
}