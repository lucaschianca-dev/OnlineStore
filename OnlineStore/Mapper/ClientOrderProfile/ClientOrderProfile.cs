using AutoMapper;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Models;

namespace OnlineStore.Mapper.ClientOrderProfile;

public class ClientOrderProfile: Profile
{
    public ClientOrderProfile()
    {
        // Mapeamento de CreateClientOrderInput para ClientOrder
        CreateMap<AddClientOrderInput, ClientOrder>()
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) // Ignorar TotalAmount, pois ele é calculado manualmente
            .ForMember(dest => dest.Items, opt => opt.Ignore()) // Ignorar Items, pois eles são mapeados manualmente
            .ForMember(dest => dest.Status, opt => opt.Ignore()); // Ignorar Status, pois você atribui manualmente

        // Mapeamento de ClientOrderService para AddClientOrderOutput
        CreateMap<ClientOrder, AddClientOrderOutput>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id)) // Mapear o Id para OrderId
            .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(_ => true)); // Sucesso sempre será true no mapeamento
    }
}
