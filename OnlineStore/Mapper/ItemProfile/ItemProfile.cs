using AutoMapper;
using OnlineStore.DTOs.Item.CriarItem;
using OnlineStore.Models;

namespace OnlineStore.Mapper.ItemProfile;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        // Mapeia de CriarItemInput para Item
        CreateMap<CriarItemInput, Item>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());  // O Id é gerado automaticamente no modelo

        // Mapeia de Item para CriarItemOutput
        CreateMap<Item, CriarItemOutput>();
    }
}