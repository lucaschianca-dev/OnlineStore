using OnlineStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories.ItemRepository
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(string id);
        Task<string> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(string id, Item item);
        Task<bool> DeleteItemAsync(string id);
    }
}