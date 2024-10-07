using AutoMapper;
using OnlineStore.DTOs.ItemOrderDto;
using OnlineStore.Models;

namespace OnlineStore.Mapper.itemOrderProfile;

public class ItemOrderProfile : Profile
{
    public ItemOrderProfile() 
    {
        // Mapeamento de ItemOrderInput para ItemOrder
        CreateMap<ItemOrderInput, ItemOrder>();
    }
}
