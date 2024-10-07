using AutoMapper;
using OnlineStore.DTOs.ClientOrderDto.AddClientOrder;
using OnlineStore.Models;

namespace OnlineStore.Mapper.ClientOrderProfile;

public class ClientOrderProfile: Profile
{
    public ClientOrderProfile()
    {
        // Mapeamento de AddClientOrderInput para ClientOrder
        CreateMap<AddClientOrderInput, ClientOrder>();

        // Mapeamento de ClientOrder para AddClientOrderOutput
        CreateMap<ClientOrder, AddClientOrderOutput>()
            .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(_ => true)); // Sucesso sempre será true no mapeamento
    }
}
