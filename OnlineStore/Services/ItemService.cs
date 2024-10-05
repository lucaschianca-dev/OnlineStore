using OnlineStore.Models;
using OnlineStore.DTOs.Item.CriarItem;
using AutoMapper;
using OnlineStore.DTOs.Item.AtualizarItem;
using OnlineStore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public class ItemService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;

        public ItemService(IMapper mapper, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetItemsAsync();
        }

        public async Task<Item> GetItemByIdAsync(string id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task<CriarItemOutput> AddItemToUserAsync(string userId, CriarItemInput input)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new CriarItemOutput { Sucesso = false, MensagemErro = "Usuário não encontrado" };
            }

            // Mapear input para o modelo Item
            var item = _mapper.Map<Item>(input);
            user.Items.Add(item);  // Adiciona o item à lista de itens do usuário

            // Atualiza o usuário com o novo item
            await _userRepository.UpdateUserAsync(userId, user);

            return new CriarItemOutput
            {
                Sucesso = true,
                Id = item.Id
            };
        }

        public async Task<CriarItemOutput> AddItemAsync(CriarItemInput input)
        {
            var item = _mapper.Map<Item>(input);
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
            var updatedItem = _mapper.Map<Item>(input);
            return await _itemRepository.UpdateItemAsync(id, updatedItem);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }
}